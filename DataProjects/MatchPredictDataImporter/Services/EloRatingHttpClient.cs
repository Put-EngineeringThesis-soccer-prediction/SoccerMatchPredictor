using MatchPredictDataImporter.Interfaces;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MatchPredictDataImporter.Services
{
	public class EloRatingHttpClient : IEloRatingHttpClient
	{
		private readonly HttpClient _client;

		public EloRatingHttpClient(HttpClient client)
		{
			_client = client;
		}

		public async Task<Stream> GetEloRatingResponse(Uri requestUri)
		{
			return await _client.GetStreamAsync(requestUri);
		}
	}
}