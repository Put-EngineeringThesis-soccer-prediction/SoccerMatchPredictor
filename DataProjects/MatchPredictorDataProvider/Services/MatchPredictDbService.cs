using MatchPredictorDataProvider.DtoModels;
using MatchPredictorDataProvider.Interfaces;
using Microsoft.EntityFrameworkCore;
using SoccerDataImporter.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPredictorDataProvider.Services
{
	public class MatchPredictDbService : IMatchPredictDbService
	{
		private readonly MatchPredictDbContext _context;

		public MatchPredictDbService(MatchPredictDbContext contex)
		{
			_context = contex;
		}

		public async Task<List<DetailedMatchWithHistory>> GetDetailedMatchesInGivenSeason(int season, int numberOfMatches, int? teamId = null)
		{
			string seasonYears = $"{season}/{season + 1}";
			var matches = new List<Match>();
			if (teamId is null)
			{
				matches = await _context.Match.Where(x => x.Season == seasonYears)
					.Include(x => x.HomeTeamApi).ThenInclude(y => y.TeamAttributesTeamApi)
					.Include(x => x.AwayTeamApi).ThenInclude(y => y.TeamAttributesTeamApi)
					.ToListAsync();
			}
			else
			{
				var team = await _context.Team.SingleAsync(x => x.Id == teamId.Value);
				matches = await _context.Match
					.Where(x => x.Season == seasonYears && (x.HomeTeamApiId == team.TeamApiId || x.AwayTeamApiId == team.TeamApiId))
					.Include(x => x.HomeTeamApi).ThenInclude(y => y.TeamAttributesTeamApi)
					.Include(x => x.AwayTeamApi).ThenInclude(y => y.TeamAttributesTeamApi)
					.OrderByDescending(x => x.Date)
					.ToListAsync();
			}
			var result = new List<DetailedMatchWithHistory>();
			var lastSeasonPointsDict = new Dictionary<int, (int? lastSeasonPoints, int seasonsPlayed)>();
			var previousEncountersDict = new Dictionary<string, List<EncounterHistoryDto>>();
			foreach (var match in matches)
			{
				var homePlayerIds = new List<int?>
				{
					match.HomePlayer1,
					match.HomePlayer2,
					match.HomePlayer3,
					match.HomePlayer4,
					match.HomePlayer5,
					match.HomePlayer6,
					match.HomePlayer7,
					match.HomePlayer8,
					match.HomePlayer9,
					match.HomePlayer10,
					match.HomePlayer11
				};

				var awayPlayersIds = new List<int?>
				{
					match.AwayPlayer1,
					match.AwayPlayer2,
					match.AwayPlayer3,
					match.AwayPlayer4,
					match.AwayPlayer5,
					match.AwayPlayer6,
					match.AwayPlayer7,
					match.AwayPlayer8,
					match.AwayPlayer9,
					match.AwayPlayer10,
					match.AwayPlayer11
				};

				if (!lastSeasonPointsDict.ContainsKey(match.HomeTeamApi.Id))
				{
					lastSeasonPointsDict.Add(match.HomeTeamApi.Id, (
						await CalculateTeamsPointsInASeason(match.HomeTeamApi.Id, season - 1),
						await GetSeasonPlayedUntilToday(match.HomeTeamApiId, match.Date.Value)));
				}

				if (!lastSeasonPointsDict.ContainsKey(match.AwayTeamApi.Id))
				{
					lastSeasonPointsDict.Add(match.AwayTeamApi.Id, (
						await CalculateTeamsPointsInASeason(match.AwayTeamApi.Id, season - 1),
						await GetSeasonPlayedUntilToday(match.AwayTeamApiId, match.Date.Value)));
				}

				string idCombo = match.HomeTeamApi.Id > match.AwayTeamApi.Id ? 
					$"{match.HomeTeamApi.Id}_{match.AwayTeamApi.Id}" : 
					$"{match.AwayTeamApi.Id}_{match.HomeTeamApi.Id}";

				if (!previousEncountersDict.ContainsKey(idCombo))
				{
					previousEncountersDict.Add(
						idCombo, 
						await FindPastEncountersSinceMatch(match.HomeTeamApi, match.AwayTeamApi, match.Date.Value));
				}

				var homeTeam = await BuildTeamDto(match.HomeTeamApi,
									  homePlayerIds,
									  match.Date.Value,
									  lastSeasonPointsDict[match.HomeTeamApi.Id]);

				var awayTeam = await BuildTeamDto(match.AwayTeamApi,
									  awayPlayersIds,
									  match.Date.Value,
									  lastSeasonPointsDict[match.AwayTeamApi.Id]);

				result.Add(new DetailedMatchWithHistory
				{
					MatchDetails = match,
					HomeTeam = homeTeam,
					AwayTeam = awayTeam,
					HomeTeamHistory = await GetTeamMatchHistoryFromGivenMatch(homeTeam, match.Date.Value, numberOfMatches, matches),
					AwayTeamHistory = await GetTeamMatchHistoryFromGivenMatch(awayTeam, match.Date.Value, numberOfMatches, matches),
					PastEncounters = previousEncountersDict[idCombo].Where(x => x.Date < match.Date).ToList()
				});
			}

			return result.OrderBy(x => x.MatchDetails.Date).ToList();
		}

		private async Task<List<EncounterHistoryDto>> FindPastEncountersSinceMatch(Team FirstTeam, Team SecondTeam, DateTime matchDate)
		{
			var encounters  = await _context.Match
				.Where(x =>
				((x.HomeTeamApiId == FirstTeam.TeamApiId && x.AwayTeamApiId == SecondTeam.TeamApiId) ||
				(x.HomeTeamApiId == SecondTeam.TeamApiId && x.AwayTeamApiId == FirstTeam.TeamApiId)) && 
				x.Date < matchDate)
				.OrderByDescending(x => x.Date)
				.ToListAsync();
			var result = new List<EncounterHistoryDto>();
			foreach(var encounter in encounters)
			{
				var homeTeam = encounter.HomeTeamApiId == FirstTeam.TeamApiId ? FirstTeam : SecondTeam;
				var awayTeam = encounter.AwayTeamApiId == FirstTeam.TeamApiId ? FirstTeam : SecondTeam;
				result.Add(new EncounterHistoryDto
				{
					HomeTeamId = homeTeam.Id,
					HomeTeamApiId = homeTeam.TeamApiId.Value,
					HomeTeamName = homeTeam.TeamLongName,
					HomeTeamGoals = encounter.HomeTeamGoal.Value,
					AwayTeamId = awayTeam.Id,
					AwayTeamApiId = awayTeam.TeamApiId.Value,
					AwayTeamName = awayTeam.TeamLongName,
					AwayTeamGoals = encounter.AwayTeamGoal.Value,
					Date = encounter.Date.Value
				});
			}

			return result;
		}

		private async Task<TeamHistory> GetTeamMatchHistoryFromGivenMatch(
			TeamDto requestedTeam, 
			DateTime matchDate, 
			int numberOfMatches, 
			List<Match> downloadedMatches)
		{

			List<Match> matchHistory = downloadedMatches
				.Where(x => (x.HomeTeamApiId == requestedTeam.TeamApiId || x.AwayTeamApiId == requestedTeam.TeamApiId) && x.Date.Value < matchDate)
				.OrderByDescending(x => x.Date)
				.Take(numberOfMatches)
				.ToList();

			int matchesFound = matchHistory.Count;
			if (matchesFound < numberOfMatches)
			{
				var lastFoundMatch = matchHistory.LastOrDefault();
				DateTime searchableDate = lastFoundMatch is null ? matchDate : lastFoundMatch.Date.Value;
				matchHistory.AddRange(await _context.Match
					.Where(x => (x.HomeTeamApiId == requestedTeam.TeamApiId || x.AwayTeamApiId == requestedTeam.TeamApiId) && x.Date.Value < searchableDate)
					.OrderByDescending(x => x.Date)
					.Take(numberOfMatches - matchesFound)
					.ToListAsync());
			}

			return new TeamHistory
			{
				MatchHistory = matchHistory
			};
		}

		private async Task<TeamDto> BuildTeamDto(Team team, List<int?> playersIds, DateTime matchDate, (int? lastSeasonPoints, int seasonsPlayed) lastSeasonInformation)
		{
			var result = new TeamDto();

			var playersWithAttributes = await _context.Player
				.Include(x => x.PlayerAttributesPlayerApi)
				.Where(x => playersIds.Contains(x.PlayerApiId))
				.ToListAsync();
			var playersDto = new List<PlayerDto>();
			foreach (var player in playersWithAttributes)
			{
				playersDto.Add(MapPlayerToPlayerDto(player, matchDate));
			}

			return new TeamDto
			{
				Id = team.Id,
				TeamApiId = team.TeamApiId.Value,
				TeamName = team.TeamLongName,
				SeasonsPlayed = lastSeasonInformation.seasonsPlayed,
				LastSeasonPoints = lastSeasonInformation.lastSeasonPoints,
				Attributes = team.TeamAttributesTeamApi
									.OrderByDescending(x => x.Date)
									.FirstOrDefault(x => x.Date.Value <= matchDate),
				Players = playersDto,
				CurrentEloRating = await GetEloRatingForTeamAndDate(team.TeamApiId.Value, matchDate)
			};
		}

		private async Task<int> GetSeasonPlayedUntilToday(int? teamApiId, DateTime matchDate)
		{
			var distinctSeasons = await _context.Match
				.Where(x => (x.HomeTeamApiId == teamApiId || x.AwayTeamApiId == teamApiId) && x.Date.Value <= matchDate)
				.Select(x => x.Season)
				.Distinct()
				.ToListAsync();
			return distinctSeasons.Count() - 1;
		}

		private PlayerDto MapPlayerToPlayerDto(Player player, DateTime matchDate)
		{
			return new PlayerDto
			{
				Id = player.Id,
				FullName = player.PlayerName,
				Birthday = player.Birthday.Value,
				Height = player.Height.Value,
				Weight = player.Weight.Value,
				Attributes = player.PlayerAttributesPlayerApi
									.OrderByDescending(x => x.Date)
									.FirstOrDefault(x => x.Date.Value <= matchDate),
			};
		}

		private async Task<EloRating> GetEloRatingForTeamAndDate(int teamId, DateTime date)
		{
			return await _context.EloRating
				.Where(x => x.TeamApiId == teamId && x.StartDate < date)
				.OrderByDescending(x => x.StartDate)
				.FirstOrDefaultAsync();
		}

		private async Task<int?> CalculateTeamsPointsInASeason(int teamId, int season)
		{
			string seasonYears = $"{season}/{season + 1}";
			var team = await _context
				.Team
				.FirstAsync(x => x.Id == teamId);
			var teamApiId = team.TeamApiId;
			var matchesInLastSeason = await _context
				.Match
				.Where(x => (x.HomeTeamApiId == teamApiId || x.AwayTeamApiId == teamApiId) && x.Season == seasonYears)
				.ToListAsync();
			if(matchesInLastSeason is null)
			{
				return null;
			}
			int result = 0;
			foreach(var match in matchesInLastSeason)
			{
				if(match.HomeTeamApiId == teamApiId)
				{
					if(match.HomeTeamGoal > match.AwayTeamGoal)
					{
						result += 3;
					}
					else if(match.HomeTeamGoal == match.AwayTeamGoal)
					{
						result += 1;
					}
				}
				else if (match.AwayTeamApiId == teamApiId)
				{
					if (match.HomeTeamGoal < match.AwayTeamGoal)
					{
						result += 3;
					}
					else if (match.HomeTeamGoal == match.AwayTeamGoal)
					{
						result += 1;
					}
				}
			}
			return result;
		}
	}
}