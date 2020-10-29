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

		[FunctionName("GetMatchesWithGivenSeason")]
		public async Task<IActionResult> GetMatchesWithGivenSeason(
			[HttpTrigger(AuthorizationLevel.Function, "get", Route = "Matches/Season/{season}")] HttpRequest req,
			int season,
			ILogger log)
		{
			log.LogInformation($"Run request for matches from season {season}/{season + 1}");
			var o = (JArray)JToken.FromObject(await _matchPredictDbService.GetMatchesInGivenSeason(season));

			return new OkObjectResult(o);
		}

		[FunctionName("GetMatchFullDetails")]
		public async Task<IActionResult> GetMatchFullDetails(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "Matches/{matchId}/")] HttpRequest req,
		int matchId,
		ILogger log)
		{
			log.LogInformation($"Run reuqest for match with id {matchId}");
			var o = (JObject)JToken.FromObject(await _matchPredictDbService.GetMatchesWithFullDetails(matchId));

			return new OkObjectResult(o);
		}

		[FunctionName("GetTeamsEloRating")]
		public async Task<IActionResult> GetTeamsEloRating(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "EloRating/Match/{matchId}/")] HttpRequest req,
		int matchId,
		ILogger log)
		{
			log.LogInformation($"Run reuqest for elo rating for teams in match with id season {matchId}");
			var o = (JObject)JToken.FromObject(await _matchPredictDbService.GetEloRatingForMatch(matchId));

			return new OkObjectResult(o);
		}
	}
}