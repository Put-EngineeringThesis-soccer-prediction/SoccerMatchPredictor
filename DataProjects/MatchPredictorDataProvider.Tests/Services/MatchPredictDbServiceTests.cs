using FluentAssertions;
using MatchPredictorDataProvider.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SoccerDataImporter.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPredictorDataProvider.Tests.Services
{
	[TestFixture]
	public class MatchPredictDbServiceTests
	{
		private MatchPredictDbService matchPredictDbService;
		private MatchPredictDbContext testDbContext;

		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<MatchPredictDbContext>()
				.UseInMemoryDatabase(databaseName: "MatchPredictDb")
				.Options;
			testDbContext = new MatchPredictDbContext(options);
			testDbContext.Database.EnsureDeleted();
			matchPredictDbService = new MatchPredictDbService(testDbContext);
		}

		[Test]
		public async Task GetDetailedMatchInGivenSeasonShouldReturnEmptyListForWrongSeason()
		{
			const int season = 2010;
			const int numberOfMatches = 1;

			var matchesInDb = new List<Match>
			{
				new Match{ Id = 2, HomeTeamApiId = 3, AwayTeamApiId = 4, Season = $"{season}/{season+1}", Date = new DateTime(2011, 1, 1) },
				new Match{ Id = 3, HomeTeamApiId = 5, AwayTeamApiId = 6, Season = $"{season}/{season+1}", Date = new DateTime(2011, 1, 1) },
			};
			testDbContext.Match.AddRange(matchesInDb);
			await testDbContext.SaveChangesAsync();
			var result = await matchPredictDbService.GetDetailedMatchesInGivenSeason(season + 1, numberOfMatches);

			result.Should().BeEmpty();
		}

		[Test]
		public async Task GetDetailedMatchInGivenSeasonShouldReturnExpectedMatches()
		{
			//Arrange
			const int season = 2010;
			const int numberOfMatches = 2;

			var expectedTeam1 = new Team { Id = 1, TeamApiId = 1, TeamFifaApiId = 1 };
			var expectedTeam2 = new Team { Id = 2, TeamApiId = 2, TeamFifaApiId = 2 };

			var teamsInDb = new List<Team>
			{
				expectedTeam1,
				expectedTeam2,
				new Team{ Id = 3, TeamApiId = 3, TeamFifaApiId = 3 },
				new Team{ Id = 4, TeamApiId = 4, TeamFifaApiId = 4 },
				new Team{ Id = 5, TeamApiId = 5, TeamFifaApiId = 5 },
				new Team{ Id = 6, TeamApiId = 6, TeamFifaApiId = 6 }
			};

			testDbContext.Team.AddRange(teamsInDb);

			var expectedAttribute1 = new TeamAttributes { Id = 1, TeamApiId = 1, TeamFifaApiId = 1, Date = new DateTime(2009, 1, 1) };
			var expectedAttribute2 = new TeamAttributes { Id = 2, TeamApiId = 2, TeamFifaApiId = 2, Date = new DateTime(2009, 1, 1) };

			var teamAtrributesInDb = new List<TeamAttributes>
			{
				expectedAttribute1,
				new TeamAttributes{ Id = 7, TeamApiId = 1, TeamFifaApiId = 1, Date = new DateTime(2010, 1, 2) },
				new TeamAttributes{ Id = 8, TeamApiId = 1, TeamFifaApiId = 1, Date = new DateTime(2008, 12, 31) },
				expectedAttribute2,
				new TeamAttributes{ Id = 9, TeamApiId = 2, TeamFifaApiId = 2, Date = new DateTime(2010, 1, 2) },
				new TeamAttributes{ Id = 10, TeamApiId = 1, TeamFifaApiId = 1, Date = new DateTime(2008, 12, 31) },
				new TeamAttributes{ Id = 3, TeamApiId = 3, TeamFifaApiId = 3, Date = new DateTime(2009, 1, 1)  },
				new TeamAttributes{ Id = 4, TeamApiId = 4, TeamFifaApiId = 4, Date = new DateTime(2009, 1, 1)  },
				new TeamAttributes{ Id = 5, TeamApiId = 5, TeamFifaApiId = 5, Date = new DateTime(2009, 1, 1)  },
				new TeamAttributes{ Id = 6, TeamApiId = 6, TeamFifaApiId = 6, Date = new DateTime(2009, 1, 1)  }
			};

			testDbContext.TeamAttributes.AddRange(teamAtrributesInDb);

			var expectedHomePlayer = new Player
			{
				Id = 1,
				PlayerApiId = 1,
				PlayerFifaApiId = 1,
				Birthday = new DateTime(1960, 1, 1),
				Height = 1,
				Weight = 1
			};

			var expectedAwayPlayer = new Player
			{
				Id = 2,
				PlayerApiId = 2,
				PlayerFifaApiId = 2,
				Birthday = new DateTime(1960, 1, 2),
				Height = 2,
				Weight = 2
			};

			testDbContext.Player.Add(expectedHomePlayer);
			testDbContext.Player.Add(expectedAwayPlayer);

			var expectedHomePlayerAttribute = new PlayerAttributes { Id = 1, PlayerApiId = 1, PlayerFifaApiId = 1, Date = new DateTime(2009, 1, 1) };
			var expectedAwayPlayerAttribute = new PlayerAttributes { Id = 2, PlayerApiId = 2, PlayerFifaApiId = 2, Date = new DateTime(2009, 1, 1) };

			var playerAtrributesInDb = new List<PlayerAttributes>
			{
				expectedHomePlayerAttribute,
				new PlayerAttributes{ Id = 7, PlayerApiId = 1, PlayerFifaApiId = 1, Date = new DateTime(2010, 1, 2) },
				new PlayerAttributes{ Id = 8, PlayerApiId = 1, PlayerFifaApiId = 1, Date = new DateTime(2008, 12, 31) },
				expectedAwayPlayerAttribute,
				new PlayerAttributes{ Id = 9, PlayerApiId = 2, PlayerFifaApiId = 2, Date = new DateTime(2010, 1, 2) },
				new PlayerAttributes{ Id = 10, PlayerApiId = 1, PlayerFifaApiId = 1, Date = new DateTime(2008, 12, 31) },
				new PlayerAttributes{ Id = 3, PlayerApiId = 3, PlayerFifaApiId = 3, Date = new DateTime(2009, 1, 1)  },
				new PlayerAttributes{ Id = 4, PlayerApiId = 4, PlayerFifaApiId = 4, Date = new DateTime(2009, 1, 1)  },
				new PlayerAttributes{ Id = 5, PlayerApiId = 5, PlayerFifaApiId = 5, Date = new DateTime(2009, 1, 1)  },
				new PlayerAttributes{ Id = 6, PlayerApiId = 6, PlayerFifaApiId = 6, Date = new DateTime(2009, 1, 1)  }
			};
			testDbContext.PlayerAttributes.AddRange(playerAtrributesInDb);

			var expectedMatch = new Match
			{
				Id = 1,
				HomeTeamApiId = 1,
				AwayTeamApiId = 2,
				Season = $"{season}/{season + 1}",
				Date = new DateTime(2010, 1, 1),
				HomePlayer1 = 1,
				AwayPlayer1 = 2
			};

			var matchesInDb = new List<Match>
			{
				expectedMatch,
				new Match{ Id = 2, HomeTeamApiId = 3, AwayTeamApiId = 4, Season = $"{season+1}/{season+2}", Date = new DateTime(2010, 1, 1) },
				new Match{ Id = 3, HomeTeamApiId = 5, AwayTeamApiId = 6, Season = $"{season+1}/{season+2}", Date = new DateTime(2010, 1, 1) },
			};

			testDbContext.Match.AddRange(matchesInDb);

			var expectedHistoricMatch1 = new Match { Id = 6, HomeTeamApiId = 1, AwayTeamApiId = 2, Season = $"{season - 1}/{season}", HomeTeamGoal = 1, AwayTeamGoal = 0, Date = new DateTime(2009, 1, 1) };

			var matchesInLastSeason = new List<Match>
			{
				expectedHistoricMatch1,
				new Match{ Id = 7, HomeTeamApiId = 2, AwayTeamApiId = 3, Season = $"{season-1}/{season}", HomeTeamGoal = 1, AwayTeamGoal = 0, Date = new DateTime(2009, 1, 1) },
				new Match{ Id = 8, HomeTeamApiId = 3, AwayTeamApiId = 4, Season = $"{season-1}/{season}", HomeTeamGoal = 1, AwayTeamGoal = 0, Date = new DateTime(2009, 1, 1) },
				new Match{ Id = 9, HomeTeamApiId = 4, AwayTeamApiId = 5, Season = $"{season-1}/{season}", HomeTeamGoal = 1, AwayTeamGoal = 0, Date = new DateTime(2009, 1, 1) },
				new Match{ Id = 10, HomeTeamApiId = 5, AwayTeamApiId = 6, Season = $"{season-1}/{season}", HomeTeamGoal = 1, AwayTeamGoal = 0, Date = new DateTime(2009, 1, 1) },
			};

			testDbContext.Match.AddRange(matchesInLastSeason);

			var expectedEloRating1 = new EloRating { Id = 1, TeamApiId = 1, StartDate = new DateTime(2009, 1, 1), Elo = 10 };
			var expectedEloRating2 = new EloRating { Id = 2, TeamApiId = 2, StartDate = new DateTime(2009, 1, 1), Elo = 20 };

			var eloRating = new List<EloRating>
			{
				expectedEloRating1,
				expectedEloRating2,
				new EloRating{ Id = 3, TeamApiId = 3, StartDate = new DateTime(2009,1,1), Elo = 30},
				new EloRating{ Id = 4, TeamApiId = 4, StartDate = new DateTime(2009,1,1), Elo = 40},
				new EloRating{ Id = 5, TeamApiId = 5, StartDate = new DateTime(2009,1,1), Elo = 50},
				new EloRating{ Id = 6, TeamApiId = 6, StartDate = new DateTime(2009,1,1), Elo = 60}
			};

			testDbContext.EloRating.AddRange(eloRating);
			await testDbContext.SaveChangesAsync();

			//Act
			var result = await matchPredictDbService.GetDetailedMatchesInGivenSeason(season, numberOfMatches);

			//Assert
			result.Should().HaveCount(1);

			var firstMatch = result.Single();

			firstMatch.PastEncounters.Should().HaveCount(1);

			firstMatch.HomeTeamHistory.MatchHistory.Should().HaveCount(1);
			firstMatch.HomeTeamHistory.MatchHistory.First().Should().BeEquivalentTo(expectedHistoricMatch1);
			firstMatch.HomeTeam.Players.Should().HaveCount(1);
			var homePlayer = firstMatch.HomeTeam.Players.First();
			homePlayer.Id.Should().Be(expectedHomePlayer.Id);
			homePlayer.Height.Should().Be(expectedHomePlayer.Height);
			homePlayer.Weight.Should().Be(expectedHomePlayer.Weight);
			homePlayer.Birthday.Should().Be(expectedHomePlayer.Birthday.Value);
			homePlayer.Attributes.Should().BeEquivalentTo(expectedHomePlayerAttribute);

			firstMatch.HomeTeam.Id.Should().Be(1);
			firstMatch.HomeTeam.LastSeasonPoints.Should().Be(3);
			firstMatch.HomeTeam.SeasonsPlayed.Should().Be(1);
			firstMatch.HomeTeam.Attributes.Should().BeEquivalentTo(expectedAttribute1);
			firstMatch.HomeTeam.CurrentEloRating.Should().BeEquivalentTo(expectedEloRating1);

			firstMatch.AwayTeamHistory.MatchHistory.Should().HaveCount(2);
			firstMatch.HomeTeamHistory.MatchHistory.First().Should().BeEquivalentTo(expectedHistoricMatch1);
			var awayPlayer = firstMatch.AwayTeam.Players.First();
			awayPlayer.Id.Should().Be(expectedAwayPlayer.Id);
			awayPlayer.Height.Should().Be(expectedAwayPlayer.Height);
			awayPlayer.Weight.Should().Be(expectedAwayPlayer.Weight);
			awayPlayer.Birthday.Should().Be(expectedAwayPlayer.Birthday.Value);
			awayPlayer.Attributes.Should().BeEquivalentTo(expectedAwayPlayerAttribute);

			firstMatch.AwayTeam.Id.Should().Be(2);
			firstMatch.AwayTeam.LastSeasonPoints.Should().Be(3);
			firstMatch.AwayTeam.SeasonsPlayed.Should().Be(1);
			firstMatch.AwayTeam.CurrentEloRating.Should().BeEquivalentTo(expectedEloRating2);
			firstMatch.AwayTeam.Attributes.Should().BeEquivalentTo(expectedAttribute2);

			firstMatch.MatchDetails.Should().BeEquivalentTo(expectedMatch);
		}

		[Test]
		public async Task GetDetailedMatchInGivenSeasonShouldReturnExpectedMatchesForTeam()
		{
			//Arrange
			const int season = 2010;
			const int numberOfMatches = 2;

			var expectedTeam1 = new Team { Id = 1, TeamApiId = 1, TeamFifaApiId = 1 };
			var expectedTeam2 = new Team { Id = 2, TeamApiId = 2, TeamFifaApiId = 2 };

			var teamsInDb = new List<Team>
			{
				expectedTeam1,
				expectedTeam2,
				new Team{ Id = 3, TeamApiId = 3, TeamFifaApiId = 3 },
				new Team{ Id = 4, TeamApiId = 4, TeamFifaApiId = 4 },
				new Team{ Id = 5, TeamApiId = 5, TeamFifaApiId = 5 },
				new Team{ Id = 6, TeamApiId = 6, TeamFifaApiId = 6 }
			};

			testDbContext.Team.AddRange(teamsInDb);

			var expectedAttribute1 = new TeamAttributes { Id = 1, TeamApiId = 1, TeamFifaApiId = 1, Date = new DateTime(2009, 1, 1) };
			var expectedAttribute2 = new TeamAttributes { Id = 2, TeamApiId = 2, TeamFifaApiId = 2, Date = new DateTime(2009, 1, 1) };

			var teamAtrributesInDb = new List<TeamAttributes>
			{
				expectedAttribute1,
				new TeamAttributes{ Id = 7, TeamApiId = 1, TeamFifaApiId = 1, Date = new DateTime(2010, 1, 2) },
				new TeamAttributes{ Id = 8, TeamApiId = 1, TeamFifaApiId = 1, Date = new DateTime(2008, 12, 31) },
				expectedAttribute2,
				new TeamAttributes{ Id = 9, TeamApiId = 2, TeamFifaApiId = 2, Date = new DateTime(2010, 1, 2) },
				new TeamAttributes{ Id = 10, TeamApiId = 1, TeamFifaApiId = 1, Date = new DateTime(2008, 12, 31) },
				new TeamAttributes{ Id = 3, TeamApiId = 3, TeamFifaApiId = 3, Date = new DateTime(2009, 1, 1)  },
				new TeamAttributes{ Id = 4, TeamApiId = 4, TeamFifaApiId = 4, Date = new DateTime(2009, 1, 1)  },
				new TeamAttributes{ Id = 5, TeamApiId = 5, TeamFifaApiId = 5, Date = new DateTime(2009, 1, 1)  },
				new TeamAttributes{ Id = 6, TeamApiId = 6, TeamFifaApiId = 6, Date = new DateTime(2009, 1, 1)  }
			};

			testDbContext.TeamAttributes.AddRange(teamAtrributesInDb);

			var expectedHomePlayer = new Player
			{
				Id = 1,
				PlayerApiId = 1,
				PlayerFifaApiId = 1,
				Birthday = new DateTime(1960, 1, 1),
				Height = 1,
				Weight = 1
			};

			var expectedAwayPlayer = new Player
			{
				Id = 2,
				PlayerApiId = 2,
				PlayerFifaApiId = 2,
				Birthday = new DateTime(1960, 1, 2),
				Height = 2,
				Weight = 2
			};

			testDbContext.Player.Add(expectedHomePlayer);
			testDbContext.Player.Add(expectedAwayPlayer);

			var expectedHomePlayerAttribute = new PlayerAttributes { Id = 1, PlayerApiId = 1, PlayerFifaApiId = 1, Date = new DateTime(2009, 1, 1) };
			var expectedAwayPlayerAttribute = new PlayerAttributes { Id = 2, PlayerApiId = 2, PlayerFifaApiId = 2, Date = new DateTime(2009, 1, 1) };

			var playerAtrributesInDb = new List<PlayerAttributes>
			{
				expectedHomePlayerAttribute,
				new PlayerAttributes{ Id = 7, PlayerApiId = 1, PlayerFifaApiId = 1, Date = new DateTime(2010, 1, 2) },
				new PlayerAttributes{ Id = 8, PlayerApiId = 1, PlayerFifaApiId = 1, Date = new DateTime(2008, 12, 31) },
				expectedAwayPlayerAttribute,
				new PlayerAttributes{ Id = 9, PlayerApiId = 2, PlayerFifaApiId = 2, Date = new DateTime(2010, 1, 2) },
				new PlayerAttributes{ Id = 10, PlayerApiId = 1, PlayerFifaApiId = 1, Date = new DateTime(2008, 12, 31) },
				new PlayerAttributes{ Id = 3, PlayerApiId = 3, PlayerFifaApiId = 3, Date = new DateTime(2009, 1, 1)  },
				new PlayerAttributes{ Id = 4, PlayerApiId = 4, PlayerFifaApiId = 4, Date = new DateTime(2009, 1, 1)  },
				new PlayerAttributes{ Id = 5, PlayerApiId = 5, PlayerFifaApiId = 5, Date = new DateTime(2009, 1, 1)  },
				new PlayerAttributes{ Id = 6, PlayerApiId = 6, PlayerFifaApiId = 6, Date = new DateTime(2009, 1, 1)  }
			};
			testDbContext.PlayerAttributes.AddRange(playerAtrributesInDb);

			var expectedMatch = new Match
			{
				Id = 1,
				HomeTeamApiId = 1,
				AwayTeamApiId = 2,
				Season = $"{season}/{season + 1}",
				Date = new DateTime(2010, 1, 1),
				HomePlayer1 = 1,
				AwayPlayer1 = 2
			};

			var matchesInDb = new List<Match>
			{
				expectedMatch,
				new Match{ Id = 5, HomeTeamApiId = 1, AwayTeamApiId = 2, Season = $"{season+1}/{season+2}", Date = new DateTime(2011, 1, 1) },
				new Match{ Id = 2, HomeTeamApiId = 3, AwayTeamApiId = 4, Season = $"{season+1}/{season+2}", Date = new DateTime(2010, 1, 1) },
				new Match{ Id = 4, HomeTeamApiId = 3, AwayTeamApiId = 4, Season = $"{season}/{season+1}", Date = new DateTime(2010, 1, 1) },
				new Match{ Id = 3, HomeTeamApiId = 5, AwayTeamApiId = 6, Season = $"{season+1}/{season+2}", Date = new DateTime(2010, 1, 1) },
			};

			testDbContext.Match.AddRange(matchesInDb);

			var expectedHistoricMatch1 = new Match { Id = 6, HomeTeamApiId = 1, AwayTeamApiId = 2, Season = $"{season - 1}/{season}", HomeTeamGoal = 1, AwayTeamGoal = 0, Date = new DateTime(2009, 1, 1) };

			var matchesInLastSeason = new List<Match>
			{
				expectedHistoricMatch1,
				new Match{ Id = 7, HomeTeamApiId = 2, AwayTeamApiId = 3, Season = $"{season-1}/{season}", HomeTeamGoal = 1, AwayTeamGoal = 0, Date = new DateTime(2009, 1, 1) },
				new Match{ Id = 8, HomeTeamApiId = 3, AwayTeamApiId = 4, Season = $"{season-1}/{season}", HomeTeamGoal = 1, AwayTeamGoal = 0, Date = new DateTime(2009, 1, 1) },
				new Match{ Id = 9, HomeTeamApiId = 4, AwayTeamApiId = 5, Season = $"{season-1}/{season}", HomeTeamGoal = 1, AwayTeamGoal = 0, Date = new DateTime(2009, 1, 1) },
				new Match{ Id = 10, HomeTeamApiId = 5, AwayTeamApiId = 6, Season = $"{season-1}/{season}", HomeTeamGoal = 1, AwayTeamGoal = 0, Date = new DateTime(2009, 1, 1) },
			};

			testDbContext.Match.AddRange(matchesInLastSeason);

			var expectedEloRating1 = new EloRating { Id = 1, TeamApiId = 1, StartDate = new DateTime(2009, 1, 1), Elo = 10 };
			var expectedEloRating2 = new EloRating { Id = 2, TeamApiId = 2, StartDate = new DateTime(2009, 1, 1), Elo = 20 };

			var eloRating = new List<EloRating>
			{
				expectedEloRating1,
				expectedEloRating2,
				new EloRating{ Id = 3, TeamApiId = 3, StartDate = new DateTime(2009,1,1), Elo = 30},
				new EloRating{ Id = 4, TeamApiId = 4, StartDate = new DateTime(2009,1,1), Elo = 40},
				new EloRating{ Id = 5, TeamApiId = 5, StartDate = new DateTime(2009,1,1), Elo = 50},
				new EloRating{ Id = 6, TeamApiId = 6, StartDate = new DateTime(2009,1,1), Elo = 60}
			};

			testDbContext.EloRating.AddRange(eloRating);
			await testDbContext.SaveChangesAsync();

			//Act
			var result = await matchPredictDbService.GetDetailedMatchesInGivenSeason(season, numberOfMatches, 1);

			//Assert
			result.Should().HaveCount(1);

			var firstMatch = result.Single();

			firstMatch.PastEncounters.Should().HaveCount(1);

			firstMatch.HomeTeamHistory.MatchHistory.Should().HaveCount(1);
			firstMatch.HomeTeamHistory.MatchHistory.First().Should().BeEquivalentTo(expectedHistoricMatch1);
			firstMatch.HomeTeam.Players.Should().HaveCount(1);
			var homePlayer = firstMatch.HomeTeam.Players.First();
			homePlayer.Id.Should().Be(expectedHomePlayer.Id);
			homePlayer.Height.Should().Be(expectedHomePlayer.Height);
			homePlayer.Weight.Should().Be(expectedHomePlayer.Weight);
			homePlayer.Birthday.Should().Be(expectedHomePlayer.Birthday.Value);
			homePlayer.Attributes.Should().BeEquivalentTo(expectedHomePlayerAttribute);

			firstMatch.HomeTeam.Id.Should().Be(1);
			firstMatch.HomeTeam.LastSeasonPoints.Should().Be(3);
			firstMatch.HomeTeam.SeasonsPlayed.Should().Be(1);
			firstMatch.HomeTeam.Attributes.Should().BeEquivalentTo(expectedAttribute1);
			firstMatch.HomeTeam.CurrentEloRating.Should().BeEquivalentTo(expectedEloRating1);

			firstMatch.AwayTeamHistory.MatchHistory.Should().HaveCount(2);
			firstMatch.HomeTeamHistory.MatchHistory.First().Should().BeEquivalentTo(expectedHistoricMatch1);
			var awayPlayer = firstMatch.AwayTeam.Players.First();
			awayPlayer.Id.Should().Be(expectedAwayPlayer.Id);
			awayPlayer.Height.Should().Be(expectedAwayPlayer.Height);
			awayPlayer.Weight.Should().Be(expectedAwayPlayer.Weight);
			awayPlayer.Birthday.Should().Be(expectedAwayPlayer.Birthday.Value);
			awayPlayer.Attributes.Should().BeEquivalentTo(expectedAwayPlayerAttribute);

			firstMatch.AwayTeam.Id.Should().Be(2);
			firstMatch.AwayTeam.LastSeasonPoints.Should().Be(3);
			firstMatch.AwayTeam.SeasonsPlayed.Should().Be(1);
			firstMatch.AwayTeam.CurrentEloRating.Should().BeEquivalentTo(expectedEloRating2);
			firstMatch.AwayTeam.Attributes.Should().BeEquivalentTo(expectedAttribute2);

			firstMatch.MatchDetails.Should().BeEquivalentTo(expectedMatch);
		}
	}
}