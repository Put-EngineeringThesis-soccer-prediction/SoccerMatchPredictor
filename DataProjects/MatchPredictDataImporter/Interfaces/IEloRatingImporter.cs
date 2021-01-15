using MatchPredictDataImporter.Models;
using System.Threading.Tasks;

namespace MatchPredictDataImporter.Interfaces
{
	public interface IEloRatingImporter
	{
		Task ImportTeamEloRatingHistory(TeamsToImportSetting teamName, string destinationDirectory);
	}
}