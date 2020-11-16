using SoccerDataImporter.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatchPredictorDataProvider.DtoModels
{
	public class DetailedMatchWithHistory
	{
		public Match MatchDetails { get; set; }
		public TeamDto HomeTeam { get; set; }
		public TeamDto AwayTeam { get; set; }
		public TeamHistory HomeTeamHistory { get; set; }
		public TeamHistory AwayTeamHistory { get; set; }
		public List<EncounterHistoryDto> PastEncounters { get; internal set; }
	}
}
