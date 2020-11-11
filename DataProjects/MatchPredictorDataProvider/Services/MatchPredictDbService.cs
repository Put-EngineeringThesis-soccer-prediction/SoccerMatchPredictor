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

		public async Task<List<DetailedMatchWithHistory>> GetDetailedMatchesInGivenSeason(int season, int numberOfMatches)
		{
			string seasonYears = $"{season}/{season + 1}";
			var matches = await _context.Match.Where(x => x.Season == seasonYears)
				.Include(x => x.HomeTeamApi).ThenInclude(y => y.TeamAttributesTeamApi)
				.Include(x => x.AwayTeamApi).ThenInclude(y => y.TeamAttributesTeamApi)
				.ToListAsync();
			var result = new List<DetailedMatchWithHistory>();
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

				var homeTeam = await BuildTeamDto(match.HomeTeamApi, homePlayerIds, match.Date.Value);

				var awayTeam = await BuildTeamDto(match.AwayTeamApi, awayPlayersIds, match.Date.Value);

				result.Add(new DetailedMatchWithHistory
				{
					MatchDetails = match,
					HomeTeam = homeTeam,
					AwayTeam = awayTeam,
					HomeTeamHistory = await GetTeamMatchHistoryFromGivenMatch(homeTeam, match.Date.Value, numberOfMatches, matches),
					AwayTeamHistory = await GetTeamMatchHistoryFromGivenMatch(awayTeam, match.Date.Value, numberOfMatches, matches)
				});
				return result;
			}
			return result;
		}

		public async Task<TeamHistory> GetTeamMatchHistoryFromGivenMatch(int teamId, int matchId, int numberOfMatches)
		{
			Match givenMatch = await _context.Match.SingleOrDefaultAsync(x => x.Id == matchId);
			Team requestedTeam = await _context.Team.SingleOrDefaultAsync(x => x.Id == teamId);

			List<Match> matchHistory = await _context.Match
				.Where(x => (x.HomeTeamApiId == requestedTeam.TeamApiId || x.AwayTeamApiId == requestedTeam.TeamApiId) && x.Date.Value < givenMatch.Date)
				.OrderByDescending(x => x.Date)
				.Take(numberOfMatches)
				.ToListAsync();


			return new TeamHistory
			{
				MatchHistory = matchHistory
			};
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

		private async Task<TeamDto> BuildTeamDto(Team team, List<int?> playersIds, DateTime matchDate)
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
				Attributes = team.TeamAttributesTeamApi
									.OrderByDescending(x => x.Date)
									.FirstOrDefault(x => x.Date.Value <= matchDate),
				Players = playersDto,
				CurrentEloRating = await GetEloRatingForTeamAndDate(team.TeamApiId.Value, matchDate)
			};
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
	}
}