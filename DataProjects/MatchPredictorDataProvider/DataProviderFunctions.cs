using MatchPredictorDataProvider.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace MatchPredictorDataProvider
{
	public class DataProviderFunctions
	{
		private readonly IMatchPredictDbService _matchPredictDbService;

		public DataProviderFunctions(IMatchPredictDbService matchPredictDbService)
		{
			_matchPredictDbService = matchPredictDbService;
		}

		[FunctionName("GetDetailedMatchesInGivenSeason")]
		public async Task<IActionResult> GetDetailedMatchesInGivenSeason(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "Matches/Season/DetailedMatch/{season}/{historicalMatches}")] HttpRequest req,
		int season,
		int historicalMatches,
		ILogger log)
		{
			log.LogInformation($"Run request for matches from season {season}/{season + 1}");
			var o = (JArray)JToken.FromObject(await _matchPredictDbService.GetDetailedMatchesInGivenSeason(season, historicalMatches));

			return new OkObjectResult(o);
		}

		[FunctionName("GetDetailedMatchesInGivenSeasonForATeam")]
		public async Task<IActionResult> GetDetailedMatchesInGivenSeasonForATeam(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "Matches/Season/DetailedMatch/{season}/{teamId}/{historicalMatches}")] HttpRequest req,
		int season,
		int teamId,
		int historicalMatches,
		ILogger log)
		{
			log.LogInformation($"Run request for matches from season {season}/{season + 1}");
			var o = (JArray)JToken.FromObject(await _matchPredictDbService.GetDetailedMatchesInGivenSeason(season, historicalMatches, teamId));

			return new OkObjectResult(o);
		}
	}
}