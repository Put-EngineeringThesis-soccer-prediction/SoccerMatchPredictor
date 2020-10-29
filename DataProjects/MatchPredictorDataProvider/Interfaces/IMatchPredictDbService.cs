using MatchPredictorDataProvider.DtoModels;
using SoccerDataImporter.DatabaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchPredictorDataProvider.Interfaces
{
	public interface IMatchPredictDbService
	{
		Task<List<Match>> GetMatchesInGivenSeason(int season);
		Task<DetailedMatch> GetMatchesWithFullDetails(int matchId);
		Task<EloRatingInMatch> GetEloRatingForMatch(int matchId);
	}
}