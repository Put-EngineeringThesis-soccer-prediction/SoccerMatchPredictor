using SoccerDataImporter.DatabaseModels;
using System.Collections.Generic;

namespace MatchPredictorDataProvider.DtoModels
{
	public class TeamHistory
	{
		public List<Match> MatchHistory { get; set; }
	}
}