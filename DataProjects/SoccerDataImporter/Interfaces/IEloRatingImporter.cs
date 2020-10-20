using SoccerDataImporter.Models;
using System.Threading.Tasks;

namespace SoccerDataImporter.Interfaces
{
	interface IEloRatingImporter
	{
		Task ImportTeamEloRatingHistory(TeamsToImportSetting teamName, string destinationDirectory);
	}
}
