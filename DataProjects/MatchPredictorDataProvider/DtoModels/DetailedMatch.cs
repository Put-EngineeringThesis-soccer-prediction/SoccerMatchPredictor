using SoccerDataImporter.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatchPredictorDataProvider.DtoModels
{
	public class DetailedMatch
	{
		public Match MatchDetails { get; set; }
		public TeamDto HomeTeam { get; set; }
		public TeamDto AwayTeam { get; set; }
		public string Winner { get; set; }
	}
}
