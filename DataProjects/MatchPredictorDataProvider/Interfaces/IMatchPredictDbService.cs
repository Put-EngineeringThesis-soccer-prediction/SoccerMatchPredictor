using MatchPredictorDataProvider.DtoModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchPredictorDataProvider.Interfaces
{
	public interface IMatchPredictDbService
	{
		Task<List<int>> GetMatcheIdsInGivenSeason(int season);
		Task<DetailedMatch> GetMatchWithFullDetails(int matchId);
		Task<TeamHistory> GetTeamMatchHistoryFromGivenMatch(int teamId, int matchId, int numberOfMatches);
	}
}