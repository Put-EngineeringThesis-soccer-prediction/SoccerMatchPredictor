using System;

namespace MatchPredictorDataProvider.DtoModels
{
	public class EncounterHistoryDto
	{
		public int HomeTeamId { get; set; }
		public int HomeTeamApiId { get; set; }
		public string HomeTeamName { get; set; }
		public int AwayTeamId { get; set; }
		public int AwayTeamApiId { get; set; }
		public string AwayTeamName { get; set; }
		public DateTime Date { get; set; }
		public int HomeTeamGoals { get; set; }
		public int AwayTeamGoals { get; set; }
	}
}