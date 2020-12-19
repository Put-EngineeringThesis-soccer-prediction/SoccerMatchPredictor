using SoccerDataImporter.Models;
using System.Threading.Tasks;

namespace SoccerDataImporter.Interfaces
{
	public interface IEloRatingImporter
	{
		Task ImportTeamEloRatingHistory(TeamsToImportSetting teamName, string destinationDirectory);
	}
}