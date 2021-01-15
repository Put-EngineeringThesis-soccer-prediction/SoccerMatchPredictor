using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using MatchPredictDataImporter.DatabaseModels;
using MatchPredictDataImporter.Interfaces;
using MatchPredictDataImporter.Models;
using MatchPredictDataImporter.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MatchPredictDataImporter.Tests.Services
{
	[TestFixture]
	public class EloRatingImporterTests
	{
		private Mock<IEloRatingHttpClient> httpClientMock;

		private MatchPredictDbContext testDbContext;

		private IEloRatingImporter eloRatingImporter;

		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<MatchPredictDbContext>()
				.UseInMemoryDatabase(databaseName: "MatchPredictDb")
				.Options;
			testDbContext = new MatchPredictDbContext(options);
			httpClientMock = new Mock<IEloRatingHttpClient>();
			eloRatingImporter = new EloRatingImporter(httpClientMock.Object, testDbContext);
		}

		[Test]
		public async Task ImportTeamEloRatingHistoryShouldImportAllEloRatingsForTestTeam()
		{
			//1946-07-07 - 1946-09-17
			string destination = "./Files/Output";
			string sourceFile = "./Files/TestEloRating.csv";
			var team = new TeamsToImportSetting
			{
				ApiTeamName = "apiTeam",
				DbTeamName = "dbTeam"
			};

			testDbContext.Match.Add(new DatabaseModels.Match { Date = new DateTime(1946, 7, 7) });
			testDbContext.Match.Add(new DatabaseModels.Match { Date = new DateTime(1946, 10, 1) });
			testDbContext.Country.Add(new Country { Id = 1 });
			testDbContext.Team.Add(new Team { Id = 1, TeamApiId = 1, TeamFifaApiId = 1, TeamLongName = team.DbTeamName });
			testDbContext.SaveChanges();

			httpClientMock.Setup(x => x.GetEloRatingResponse(new Uri("http://api.clubelo.com/" + team.ApiTeamName)))
				.ReturnsAsync(File.OpenRead(sourceFile));

			await eloRatingImporter.ImportTeamEloRatingHistory(team, destination);

			var addedRatings = await testDbContext.EloRating.ToListAsync();
			addedRatings.Should().HaveCount(5);
			addedRatings.TrueForAll(x => x.TeamApiId == 1 && x.CountryId == 1);

			testDbContext.Database.EnsureDeleted();
		}

		[Test]
		public async Task ImportTeamEloRatingHistoryShouldImportEloRatingsForTestTeamWithRelevantDates()
		{
			string destination = "./Files/Output";
			string sourceFile = "./Files/TestEloRating.csv";
			var team = new TeamsToImportSetting
			{
				ApiTeamName = "apiTeam",
				DbTeamName = "dbTeam"
			};

			testDbContext.Match.Add(new DatabaseModels.Match { Date = new DateTime(1945, 9, 7) });
			testDbContext.Match.Add(new DatabaseModels.Match { Date = new DateTime(1945, 9, 11) });
			testDbContext.Country.Add(new Country { Id = 1 });
			testDbContext.Team.Add(new Team { Id = 1, TeamApiId = 1, TeamFifaApiId = 1, TeamLongName = team.DbTeamName });
			testDbContext.SaveChanges();

			httpClientMock.Setup(x => x.GetEloRatingResponse(new Uri("http://api.clubelo.com/" + team.ApiTeamName)))
				.ReturnsAsync(File.OpenRead(sourceFile));

			await eloRatingImporter.ImportTeamEloRatingHistory(team, destination);

			var addedRatings = await testDbContext.EloRating.ToListAsync();
			addedRatings.Should().HaveCount(2);
			addedRatings.TrueForAll(x => x.TeamApiId == 1 && x.CountryId == 1);
			testDbContext.Database.EnsureDeleted();
		}

		[Test]
		public async Task ImportTeamEloRatingHistoryShouldNotImportEloRatings()
		{
			string destination = "./Files/Output";
			string sourceFile = "./Files/TestEloRating.csv";
			var team = new TeamsToImportSetting
			{
				ApiTeamName = "apiTeam",
				DbTeamName = "dbTeam"
			};

			testDbContext.Match.Add(new DatabaseModels.Match { Date = new DateTime(1943, 9, 7) });
			testDbContext.Match.Add(new DatabaseModels.Match { Date = new DateTime(1943, 9, 11) });
			testDbContext.Country.Add(new Country { Id = 1 });
			testDbContext.Team.Add(new Team { Id = 1, TeamApiId = 1, TeamFifaApiId = 1, TeamLongName = team.DbTeamName });
			testDbContext.SaveChanges();

			httpClientMock.Setup(x => x.GetEloRatingResponse(new Uri("http://api.clubelo.com/" + team.ApiTeamName)))
				.ReturnsAsync(File.OpenRead(sourceFile));

			await eloRatingImporter.ImportTeamEloRatingHistory(team, destination);

			var addedRatings = await testDbContext.EloRating.ToListAsync();
			addedRatings.Should().BeEmpty();
		}
	}
}