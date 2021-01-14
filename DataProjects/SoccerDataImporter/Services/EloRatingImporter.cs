using Microsoft.EntityFrameworkCore;
using SoccerDataImporter.DatabaseModels;
using SoccerDataImporter.Interfaces;
using SoccerDataImporter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoccerDataImporter.Services
{
	public class EloRatingImporter : IEloRatingImporter
	{
		private readonly IEloRatingHttpClient _httpClient;
		private const string apiBaseUri = "http://api.clubelo.com/";
		private readonly MatchPredictDbContext _dbContext;

		public EloRatingImporter(IEloRatingHttpClient httpClient, MatchPredictDbContext dbContext)
		{
			_httpClient = httpClient;
			_dbContext = dbContext;
		}

		public async Task ImportTeamEloRatingHistory(TeamsToImportSetting teams, string destinationDirectory)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"start processing {teams.ApiTeamName}");
			Console.ResetColor();
			var fileDestination = $"{destinationDirectory}/{teams.ApiTeamName}.csv";
			if (!await SaveToCsvFile(fileDestination, new Uri(apiBaseUri + teams.ApiTeamName)))
			{
				return;
			}
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"saved {teams.ApiTeamName} file to {fileDestination}");
			Console.ResetColor();
			var eloRatingList = GetEloRatingListFromCsvFile(fileDestination);
			await ImportEloRatingToDatabase(eloRatingList, teams.DbTeamName);
		}

		private async Task ImportEloRatingToDatabase(List<EloRatingApiModel> eloRatingList, string dbTeamName)
		{
			var teamFromDb = await _dbContext.Team.SingleOrDefaultAsync(x => dbTeamName == x.TeamLongName);
			var country = await _dbContext.Country.SingleAsync();
			var eloRatingsToInsert = eloRatingList.Select(x => EloRating.GetDbFromEloRating(x, teamFromDb, country, 0)).ToList();
			await _dbContext.AddRangeAsync(eloRatingsToInsert);
			await _dbContext.SaveChangesAsync();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"saved {dbTeamName} file to database, inserted {eloRatingsToInsert.Count}");
			Console.ResetColor();
			Console.WriteLine($"----------------------------------------------------");
		}

		private List<EloRatingApiModel> GetEloRatingListFromCsvFile(string fileDestination)
		{
			var earliestDate = _dbContext.Match.OrderBy(x => x.Date).First().Date.Value;
			var latestDate = _dbContext.Match.OrderBy(x => x.Date).Last().Date.Value;

			return File.ReadAllLines(fileDestination)
				.Skip(1)
				.Select(v => EloRatingApiModel.FromCsv(v))
				.Where(v => v != null && v.From > earliestDate.AddYears(-1) && v.To < latestDate.AddYears(1))
				.ToList();
		}

		private async Task<bool> SaveToCsvFile(string fileName, Uri teamUri)
		{
			Directory.CreateDirectory(fileName.Substring(0, fileName.LastIndexOf("/")));
			try
			{
				using (var responseStream = await _httpClient.GetEloRatingResponse(teamUri))
				{
					using (var fileStream = File.Create(fileName))
					{
						responseStream.CopyTo(fileStream);
					}
				}
			}
			catch (HttpRequestException ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Error while getting request for team {teamUri.AbsoluteUri}: {ex.Message}");
				Console.ResetColor();
				return false;
			}
			return true;
		}
	}
}