using MatchPredictorDataProvider.DtoModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchPredictorDataProvider.Interfaces
{
	public interface IMatchPredictDbService
	{
		Task<List<DetailedMatchWithHistory>> GetDetailedMatchesInGivenSeason(int season, int numberOfMatches, int? teamId = null);
	}
}