using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using MatchPredictDataImporter.DatabaseModels;
using MatchPredictDataImporter.Interfaces;
using MatchPredictDataImporter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPredictDataImporter.Tests.Services
{
	[TestFixture]
	public class FootballDataImporterTests
	{
		private MatchPredictDbContext testDbContext;

		private IFootballDataImporter footballDataImporter;

		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<MatchPredictDbContext>()
				.UseInMemoryDatabase(databaseName: "MatchPredictDb")
				.Options;
			testDbContext = new MatchPredictDbContext(options);
			testDbContext.Database.EnsureDeleted();
			footballDataImporter = new FootballDataImporter(testDbContext);
		}

		[Test]
		public async Task ImportTeamFootballDataMissingValuesShouldFillFieldsAccordingToTestFile()
		{
			//Arsenal,West Brom,Bolton,Stoke,Everton,Blackburn
			string sourceDirectory = "./Files/FootballData/";
			var teamDict = new Dictionary<string, string>
			{
				{"Arsenal", "dbTeam1" },
				{"West Brom", "dbTeam2" },
				{"Bolton", "dbTeam3" },
				{"Stoke", "dbTeam4" },
				{"Everton", "dbTeam5" },
				{"Blackburn", "dbTeam6" }
			};

			var teamsInDatabase = new List<Team>
			{
				new Team { Id = 1, TeamApiId = 1, TeamFifaApiId =1, TeamLongName = "dbTeam1" },
				new Team { Id = 2, TeamApiId = 2, TeamFifaApiId =2, TeamLongName = "dbTeam2" },
				new Team { Id = 3, TeamApiId = 3, TeamFifaApiId =3, TeamLongName = "dbTeam3" },
				new Team { Id = 4, TeamApiId = 4, TeamFifaApiId =4, TeamLongName = "dbTeam4" },
				new Team { Id = 5, TeamApiId = 5, TeamFifaApiId =5, TeamLongName = "dbTeam5" },
				new Team { Id = 6, TeamApiId = 6, TeamFifaApiId =6, TeamLongName = "dbTeam6" },
			};
			var matchesInDatabase = new List<Match>
			{
				new Match {Id = 1, HomeTeamApiId = 1, AwayTeamApiId = 2, Date = new DateTime(2008,8,16)},
				new Match {Id = 2, HomeTeamApiId = 3, AwayTeamApiId = 4, Date = new DateTime(2008,8,16)},
				new Match {Id = 3, HomeTeamApiId = 5, AwayTeamApiId = 6, Date = new DateTime(2008,8,16)}
			};
			testDbContext.Match.AddRange(matchesInDatabase);
			testDbContext.Team.AddRange(teamsInDatabase);
			testDbContext.SaveChanges();

			var expectedMatchList = new List<Match>
			{
				new Match {
					Id = 1, HomeTeamApiId = 1, AwayTeamApiId = 2, Date = new DateTime(2008,8,16),
					HomeTeamCorners = 7,
					HomeTeamShots = 24,
					HomeTeamShotsOnTarget = 14,
					HomeTeamFoulsCommitted = 11,
					HomeTeamYellowCards = 0,
					HomeTeamRedCards = 0,
					AwayTeamCorners = 5,
					AwayTeamShots = 5,
					AwayTeamShotsOnTarget = 4,
					AwayTeamFoulsCommitted = 8,
					AwayTeamYellowCards = 0,
					AwayTeamRedCards = 0,
				},
				new Match {
					Id = 2, HomeTeamApiId = 3, AwayTeamApiId = 4, Date = new DateTime(2008,8,16),
					HomeTeamCorners = 4,
					HomeTeamShots = 14,
					HomeTeamShotsOnTarget = 8,
					HomeTeamFoulsCommitted = 13,
					HomeTeamYellowCards = 1,
					HomeTeamRedCards = 0,
					AwayTeamCorners = 3,
					AwayTeamShots = 8,
					AwayTeamShotsOnTarget = 2,
					AwayTeamFoulsCommitted = 12,
					AwayTeamYellowCards = 2,
					AwayTeamRedCards = 0,
				},
				new Match {
					Id = 3, HomeTeamApiId = 5, AwayTeamApiId = 6, Date = new DateTime(2008,8,16),
					HomeTeamCorners = 3,
					HomeTeamShots = 10,
					HomeTeamShotsOnTarget = 5,
					HomeTeamFoulsCommitted = 11,
					HomeTeamYellowCards = 2,
					HomeTeamRedCards = 0,
					AwayTeamCorners = 5,
					AwayTeamShots = 15,
					AwayTeamShotsOnTarget = 11,
					AwayTeamFoulsCommitted = 9,
					AwayTeamYellowCards = 2,
					AwayTeamRedCards = 0,
				}
			};

			await footballDataImporter.ImportTeamFootballDataMissingValues(sourceDirectory, teamDict);

			var newMatchList = await testDbContext.Match.ToListAsync();
			newMatchList.ForEach(x =>
			{
				x.HomeTeamApi = null;
				x.AwayTeamApi = null;
			});
			newMatchList.Should().BeEquivalentTo(expectedMatchList);

			testDbContext.Database.EnsureDeleted();
		}

		[Test]
		public async Task ImportTeamFootballDataMissingValuesShouldNotChangeDifferentMatch()
		{
			//Arsenal,West Brom,Bolton,Stoke,Everton,Blackburn
			string sourceDirectory = "./Files/FootballData/";
			var teamDict = new Dictionary<string, string>
			{
				{"Arsenal", "dbTeam1" },
				{"West Brom", "dbTeam2" },
				{"Bolton", "dbTeam3" },
				{"Stoke", "dbTeam4" },
				{"Everton", "dbTeam5" },
				{"Blackburn", "dbTeam6" }
			};

			var notChangedMatch = new Match
			{
				Id = 1,
				HomeTeamApiId = 1,
				AwayTeamApiId = 2,
				Date = new DateTime(2008, 8, 17)
			};

			var teamsInDatabase = new List<Team>
			{
				new Team { Id = 1, TeamApiId = 1, TeamFifaApiId =1, TeamLongName = "dbTeam1" },
				new Team { Id = 2, TeamApiId = 2, TeamFifaApiId =2, TeamLongName = "dbTeam2" },
				new Team { Id = 3, TeamApiId = 3, TeamFifaApiId =3, TeamLongName = "dbTeam3" },
				new Team { Id = 4, TeamApiId = 4, TeamFifaApiId =4, TeamLongName = "dbTeam4" },
				new Team { Id = 5, TeamApiId = 5, TeamFifaApiId =5, TeamLongName = "dbTeam5" },
				new Team { Id = 6, TeamApiId = 6, TeamFifaApiId =6, TeamLongName = "dbTeam6" },
			};
			var matchesInDatabase = new List<Match>
			{
				notChangedMatch,
				new Match {Id = 2, HomeTeamApiId = 3, AwayTeamApiId = 4, Date = new DateTime(2008,8,16)},
				new Match {Id = 3, HomeTeamApiId = 5, AwayTeamApiId = 6, Date = new DateTime(2008,8,16)},
				new Match {Id = 4, HomeTeamApiId = 1, AwayTeamApiId = 2, Date = new DateTime(2008,8,16)}
			};
			testDbContext.Match.AddRange(matchesInDatabase);
			testDbContext.Team.AddRange(teamsInDatabase);
			testDbContext.SaveChanges();

			await footballDataImporter.ImportTeamFootballDataMissingValues(sourceDirectory, teamDict);

			var unchangedMatch = testDbContext.Match.First(x => x.Id == 1);
			unchangedMatch.Should().BeEquivalentTo(notChangedMatch);
			testDbContext.Database.EnsureDeleted();
		}
	}
}