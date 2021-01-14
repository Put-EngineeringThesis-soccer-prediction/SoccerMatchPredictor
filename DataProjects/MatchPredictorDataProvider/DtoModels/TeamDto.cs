using SoccerDataImporter.DatabaseModels;
using System.Collections.Generic;

namespace MatchPredictorDataProvider.DtoModels
{
	public class TeamDto
	{
		public int Id { get; set; }
		public int TeamApiId { get; set; }
		public string TeamName { get; set; }
		public int SeasonsPlayed { get; set; }
		public int? LastSeasonPoints { get; set; }
		public List<PlayerDto> Players { get; set; }
		public TeamAttributes Attributes { get; set; }
		public EloRating CurrentEloRating { get; set; }
	}
}