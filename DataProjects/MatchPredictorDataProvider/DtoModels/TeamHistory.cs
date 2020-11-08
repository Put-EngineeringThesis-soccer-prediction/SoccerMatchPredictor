using System.Collections.Generic;

namespace MatchPredictorDataProvider.DtoModels
{
	public class TeamHistory
	{
		public int TeamId { get; set; }
		public List<DetailedMatch> MatchHistory { get; set;}
	}
}
