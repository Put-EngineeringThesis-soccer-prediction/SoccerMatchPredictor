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

		public async Task<List<int>> GetMatcheIdsInGivenSeason(int season)
		{
			string seasonYears = $"{season}/{season + 1}";
			return await _context.Match.Where(x => x.Season == seasonYears).Select(x => x.Id).ToListAsync();
		}

		public async Task<DetailedMatch> GetMatchWithFullDetails(int matchId)
		{
			var result = new DetailedMatch();
			var match = await _context.Match.SingleOrDefaultAsync(x => x.Id == matchId);
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

			return new DetailedMatch
			{
				MatchDetails = match,
				HomeTeam = await BuildTeamDto(match.HomeTeamApiId.Value, homePlayerIds, match.Date.Value),
				AwayTeam = await BuildTeamDto(match.AwayTeamApiId.Value, awayPlayersIds, match.Date.Value)
			};
		}

		public async Task<TeamHistory> GetTeamMatchHistoryFromGivenMatch(int teamId, int matchId, int numberOfMatches)
		{
			Match givenMatch = await _context.Match.SingleOrDefaultAsync(x => x.Id == matchId);
			Team requestedTeam = await _context.Team.SingleOrDefaultAsync(x => x.Id == teamId);

			List<int> matchHistoryIds = await _context.Match
				.Where(x => (x.HomeTeamApiId == requestedTeam.TeamApiId || x.AwayTeamApiId == requestedTeam.TeamApiId) && x.Date.Value < givenMatch.Date)
				.OrderByDescending(x => x.Date)
				.Select(x => x.Id)
				.Take(numberOfMatches)
				.ToListAsync();

			List<DetailedMatch> matches = new List<DetailedMatch>();

			foreach(int historiaclMatchId in matchHistoryIds)
			{
				matches.Add(await GetMatchWithFullDetails(historiaclMatchId));
			}

			return new TeamHistory
			{
				TeamId = teamId,
				MatchHistory = matches
			};
		}

		private async Task<TeamDto> BuildTeamDto(int teamId, List<int?> playersIds, DateTime matchDate)
		{
			var result = new TeamDto();

			var team = await _context.Team
				.Include(x => x.TeamAttributesTeamApi)
				.FirstOrDefaultAsync(x => x.TeamApiId == teamId);

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