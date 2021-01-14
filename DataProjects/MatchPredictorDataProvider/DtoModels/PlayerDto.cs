using SoccerDataImporter.DatabaseModels;
using System;

namespace MatchPredictorDataProvider.DtoModels
{
	public class PlayerDto
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public DateTime Birthday { get; set; }
		public int Height { get; set; }
		public int Weight { get; set; }
		public PlayerAttributes Attributes { get; set; }
	}
}