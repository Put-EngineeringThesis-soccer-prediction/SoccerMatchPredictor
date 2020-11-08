using MatchPredictorDataProvider.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
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

		[FunctionName("GetMatchesWithGivenSeason")]
		public async Task<IActionResult> GetMatchesWithGivenSeason(
			[HttpTrigger(AuthorizationLevel.Function, "get", Route = "Matches/Season/{season}")] HttpRequest req,
			int season,
			ILogger log)
		{
			log.LogInformation($"Run request for matches from season {season}/{season + 1}");
			var o = (JArray)JToken.FromObject(await _matchPredictDbService.GetMatcheIdsInGivenSeason(season));

			return new OkObjectResult(o);
		}

		[FunctionName("GetMatchFullDetails")]
		public async Task<IActionResult> GetMatchFullDetails(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "Matches/{matchId}/")] HttpRequest req,
		int matchId,
		ILogger log)
		{
			log.LogInformation($"Run reuqest for match with id {matchId}");
			var o = (JObject)JToken.FromObject(await _matchPredictDbService.GetMatchWithFullDetails(matchId));

			return new OkObjectResult(o);
		}

		[FunctionName("GetTeamMatchHistory")]
		public async Task<IActionResult> GetTeamMatchHistory(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "Matches/{matchId}/{teamId}/{numberOfMatches}")] HttpRequest req,
		int matchId,
		int teamId,
		int numberOfMatches,
		ILogger log)
		{
			log.LogInformation($"Run reuqest for match history for team {teamId} from match with id {matchId}. Getting maximum of {numberOfMatches} games back.");
			var o = (JObject)JToken.FromObject(await _matchPredictDbService.GetTeamMatchHistoryFromGivenMatch(teamId, matchId, numberOfMatches));

			return new OkObjectResult(o);
		}
	}
}