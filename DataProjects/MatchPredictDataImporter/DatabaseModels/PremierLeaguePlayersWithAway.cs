using System;

namespace MatchPredictDataImporter.DatabaseModels
{
	public partial class PremierLeaguePlayersWithAway
	{
		public int Id { get; set; }
		public int? PlayerApiId { get; set; }
		public string PlayerName { get; set; }
		public int? PlayerFifaApiId { get; set; }
		public DateTime? Birthday { get; set; }
		public int? Height { get; set; }
		public int? Weight { get; set; }
	}
}