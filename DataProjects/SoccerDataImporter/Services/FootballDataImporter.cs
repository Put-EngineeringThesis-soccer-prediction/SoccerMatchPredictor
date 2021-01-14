using Microsoft.EntityFrameworkCore;
using SoccerDataImporter.DatabaseModels;
using SoccerDataImporter.Interfaces;
using SoccerDataImporter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerDataImporter.Services
{
	public class FootballDataImporter : IFootballDataImporter
	{
		private readonly MatchPredictDbContext _dbContext;

		public FootballDataImporter(MatchPredictDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task ImportTeamFootballDataMissingValues(string sourceDirectory, Dictionary<string, string> footballToDbDict)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"start processing {sourceDirectory}");
			Console.ResetColor();
			string[] filePaths = Directory.GetFiles(sourceDirectory);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"found {filePaths.Length} files in {sourceDirectory}");
			Console.ResetColor();
			int readMatchCount = 0;
			foreach (var file in filePaths)
			{
				var footballData = GetMatchesFromCsvFiles(file);
				var dbMatchesBatch = new List<Match>();
				foreach (var matchFromCsv in footballData)
				{
					var matchFromDb = await GetMatchFromDb(matchFromCsv, footballToDbDict);

					dbMatchesBatch.Add(AlterMatch(matchFromDb, matchFromCsv));
				}
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine($"found {dbMatchesBatch.Count} matches in db from file {file}");
				Console.ResetColor();
				_dbContext.UpdateRange(dbMatchesBatch);
				await _dbContext.SaveChangesAsync();
				readMatchCount += dbMatchesBatch.Count;
			}
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"found {readMatchCount} matches in db from files");
			Console.ResetColor();
		}

		private Match AlterMatch(Match matchFromDb, FootballDataModel matchFromCsv)
		{
			matchFromDb.HomeTeamCorners = matchFromCsv.HomeTeamCorners;
			matchFromDb.HomeTeamShots = matchFromCsv.HomeTeamShots;
			matchFromDb.HomeTeamShotsOnTarget = matchFromCsv.HomeTeamShotsOnTarget;
			matchFromDb.HomeTeamFoulsCommitted = matchFromCsv.HomeTeamFoulsCommitted;
			matchFromDb.HomeTeamYellowCards = matchFromCsv.HomeTeamYellowCards;
			matchFromDb.HomeTeamRedCards = matchFromCsv.HomeTeamRedCards;

			matchFromDb.AwayTeamCorners = matchFromCsv.AwayTeamCorners;
			matchFromDb.AwayTeamShots = matchFromCsv.AwayTeamShots;
			matchFromDb.AwayTeamShotsOnTarget = matchFromCsv.AwayTeamShotsOnTarget;
			matchFromDb.AwayTeamFoulsCommitted = matchFromCsv.AwayTeamFoulsCommitted;
			matchFromDb.AwayTeamYellowCards = matchFromCsv.AwayTeamYellowCards;
			matchFromDb.AwayTeamRedCards = matchFromCsv.AwayTeamRedCards;
			return matchFromDb;
		}

		private async Task<Match> GetMatchFromDb(FootballDataModel matchFromCsv, Dictionary<string, string> footballToDbDict)
		{
			var homeTeam = await _dbContext.Team.FirstAsync(x => x.TeamLongName == footballToDbDict[matchFromCsv.HomeTeam]);
			var awayTeam = await _dbContext.Team.FirstAsync(x => x.TeamLongName == footballToDbDict[matchFromCsv.AwayTeam]);
			return await _dbContext.Match.FirstAsync(x => x.HomeTeamApiId == homeTeam.TeamApiId && x.AwayTeamApiId == awayTeam.TeamApiId && x.Date == matchFromCsv.Date);
		}

		private List<FootballDataModel> GetMatchesFromCsvFiles(string file)
		{
			var header = File.ReadAllLines(file).First<string>().Split(',').ToList();

			return File.ReadAllLines(file)
				.Skip(1)
				.Select(v => FootballDataModel.FromCsv(v, header))
				.Where(v => v != null)
				.ToList();
		}
	}
}