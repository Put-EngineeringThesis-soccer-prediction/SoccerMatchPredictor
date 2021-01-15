using System;
using System.IO;
using System.Threading.Tasks;

namespace MatchPredictDataImporter.Interfaces
{
	public interface IEloRatingHttpClient
	{
		public Task<Stream> GetEloRatingResponse(Uri requestUri);
	}
}