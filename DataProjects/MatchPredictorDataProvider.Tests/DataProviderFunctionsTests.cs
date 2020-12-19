using FluentAssertions;
using MatchPredictorDataProvider.DtoModels;
using MatchPredictorDataProvider.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchPredictorDataProvider.Tests
{
	[TestFixture]
	public class DataProviderFunctionsTests
	{
		private Mock<ILogger> loggerMock;
		private Mock<IMatchPredictDbService> dbServiceMock;

		private DataProviderFunctions dataProviderFunctions;

		[SetUp]
		public void Setup()
		{
			loggerMock = new Mock<ILogger>();
			dbServiceMock = new Mock<IMatchPredictDbService>();
			dataProviderFunctions = new DataProviderFunctions(dbServiceMock.Object);
		}

		[Test]
		public async Task GetDetailedMatchesInGivenSeasonShouldReturnEmptyOkResponseWhenNoMatches()
		{
			Mock<HttpRequest> testRequest = new Mock<HttpRequest>();

			List<DetailedMatchWithHistory> resonseModel = new List<DetailedMatchWithHistory>();

			dbServiceMock
				.Setup(x => x.GetDetailedMatchesInGivenSeason(2010, 0, null))
				.ReturnsAsync(resonseModel);

			var response = (OkObjectResult)await dataProviderFunctions.GetDetailedMatchesInGivenSeason(testRequest.Object, 2010, 0, loggerMock.Object);
			response.StatusCode.Should().Be(200);
			JArray responeValue = (JArray)response.Value;
			responeValue.Count.Should().Be(0);
			dbServiceMock.Verify(x => x.GetDetailedMatchesInGivenSeason(2010, 0, null), Times.Once);
		}

		[Test]
		public async Task GetDetailedMatchesInGivenSeasonShouldReturnOkResponseWhenMatchesExists()
		{
			Mock<HttpRequest> testRequest = new Mock<HttpRequest>();

			var expectedMatch = new DetailedMatchWithHistory
			{
				AwayTeam = new TeamDto
				{
					Id = 1
				},
				HomeTeam = new TeamDto
				{
					Id = 1
				}
			};

			List<DetailedMatchWithHistory> resonseModel = new List<DetailedMatchWithHistory>
			{
				expectedMatch,
				new DetailedMatchWithHistory(),
				new DetailedMatchWithHistory(),
				new DetailedMatchWithHistory()
			};

			dbServiceMock
				.Setup(x => x.GetDetailedMatchesInGivenSeason(2010, 0, null))
				.ReturnsAsync(resonseModel);

			var response = (OkObjectResult)await dataProviderFunctions.GetDetailedMatchesInGivenSeason(testRequest.Object, 2010, 0, loggerMock.Object);
			response.StatusCode.Should().Be(200);
			JArray responeValue = (JArray)response.Value;
			responeValue.Count.Should().Be(4);
			var firstMatch = responeValue.First.ToObject<DetailedMatchWithHistory>();
			firstMatch.Should().BeEquivalentTo(expectedMatch);
			dbServiceMock.Verify(x => x.GetDetailedMatchesInGivenSeason(2010, 0, null), Times.Once);
		}

		[Test]
		public async Task GetDetailedMatchesInGivenSeasonForATeamShouldReturnEmptyOkResponseWhenNoMatches()
		{
			Mock<HttpRequest> testRequest = new Mock<HttpRequest>();

			List<DetailedMatchWithHistory> resonseModel = new List<DetailedMatchWithHistory>();

			dbServiceMock
				.Setup(x => x.GetDetailedMatchesInGivenSeason(2010, 0, 1))
				.ReturnsAsync(resonseModel);

			var response = (OkObjectResult)await dataProviderFunctions.GetDetailedMatchesInGivenSeasonForATeam(testRequest.Object, 2010, 1, 0, loggerMock.Object);
			response.StatusCode.Should().Be(200);
			JArray responeValue = (JArray)response.Value;
			responeValue.Count.Should().Be(0);
			dbServiceMock.Verify(x => x.GetDetailedMatchesInGivenSeason(2010, 0, 1), Times.Once);
		}

		[Test]
		public async Task GetDetailedMatchesInGivenSeasonForATeamShouldReturnOkResponseWhenMatchesExists()
		{
			Mock<HttpRequest> testRequest = new Mock<HttpRequest>();

			var expectedMatch = new DetailedMatchWithHistory
			{
				AwayTeam = new TeamDto
				{
					Id = 1
				},
				HomeTeam = new TeamDto
				{
					Id = 1
				}
			};

			List<DetailedMatchWithHistory> resonseModel = new List<DetailedMatchWithHistory>
			{
				expectedMatch,
				new DetailedMatchWithHistory(),
				new DetailedMatchWithHistory(),
				new DetailedMatchWithHistory()
			};

			dbServiceMock
				.Setup(x => x.GetDetailedMatchesInGivenSeason(2010, 0, 1))
				.ReturnsAsync(resonseModel);

			var response = (OkObjectResult)await dataProviderFunctions.GetDetailedMatchesInGivenSeasonForATeam(testRequest.Object, 2010, 1, 0, loggerMock.Object);
			response.StatusCode.Should().Be(200);
			JArray responeValue = (JArray)response.Value;
			responeValue.Count.Should().Be(4);
			var firstMatch = responeValue.First.ToObject<DetailedMatchWithHistory>();
			firstMatch.Should().BeEquivalentTo(expectedMatch);
			dbServiceMock.Verify(x => x.GetDetailedMatchesInGivenSeason(2010, 0, 1), Times.Once);
		}
	}
}