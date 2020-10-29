using System;
using System.Collections.Generic;
using System.Text;

namespace MatchPredictorDataProvider.DtoModels
{
	public class EloRatingInMatch
	{
		public DateTime HomeTeamEloRatingDate { get; set; }
		public int HomeTeamId{ get; set; }
		public decimal HomeTeamEloRating { get; set; }
		public DateTime AwayTeamEloRatingDate { get; set; }
		public int AwayTeamId { get; set; }
		public decimal AwayTeamEloRating { get; set; }
	}
}
