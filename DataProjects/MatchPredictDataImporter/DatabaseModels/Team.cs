using System.Collections.Generic;

namespace MatchPredictDataImporter.DatabaseModels
{
	public partial class Team
	{
		public Team()
		{
			EloRating = new HashSet<EloRating>();
			MatchAwayTeamApi = new HashSet<Match>();
			MatchHomeTeamApi = new HashSet<Match>();
			TeamAttributesTeamApi = new HashSet<TeamAttributes>();
			TeamAttributesTeamFifaApi = new HashSet<TeamAttributes>();
		}

		public int Id { get; set; }
		public int? TeamApiId { get; set; }
		public int? TeamFifaApiId { get; set; }
		public string TeamLongName { get; set; }
		public string TeamShortName { get; set; }

		public virtual ICollection<EloRating> EloRating { get; set; }
		public virtual ICollection<Match> MatchAwayTeamApi { get; set; }
		public virtual ICollection<Match> MatchHomeTeamApi { get; set; }
		public virtual ICollection<TeamAttributes> TeamAttributesTeamApi { get; set; }
		public virtual ICollection<TeamAttributes> TeamAttributesTeamFifaApi { get; set; }
	}
}