using System;
using System.Collections.Generic;

namespace SoccerDataImporter.DatabaseModels
{
	public partial class Player
	{
		public Player()
		{
			MatchAwayPlayer10Navigation = new HashSet<Match>();
			MatchAwayPlayer11Navigation = new HashSet<Match>();
			MatchAwayPlayer1Navigation = new HashSet<Match>();
			MatchAwayPlayer2Navigation = new HashSet<Match>();
			MatchAwayPlayer3Navigation = new HashSet<Match>();
			MatchAwayPlayer4Navigation = new HashSet<Match>();
			MatchAwayPlayer5Navigation = new HashSet<Match>();
			MatchAwayPlayer6Navigation = new HashSet<Match>();
			MatchAwayPlayer7Navigation = new HashSet<Match>();
			MatchAwayPlayer8Navigation = new HashSet<Match>();
			MatchAwayPlayer9Navigation = new HashSet<Match>();
			MatchHomePlayer10Navigation = new HashSet<Match>();
			MatchHomePlayer11Navigation = new HashSet<Match>();
			MatchHomePlayer1Navigation = new HashSet<Match>();
			MatchHomePlayer2Navigation = new HashSet<Match>();
			MatchHomePlayer3Navigation = new HashSet<Match>();
			MatchHomePlayer4Navigation = new HashSet<Match>();
			MatchHomePlayer5Navigation = new HashSet<Match>();
			MatchHomePlayer6Navigation = new HashSet<Match>();
			MatchHomePlayer7Navigation = new HashSet<Match>();
			MatchHomePlayer8Navigation = new HashSet<Match>();
			MatchHomePlayer9Navigation = new HashSet<Match>();
			PlayerAttributesPlayerApi = new HashSet<PlayerAttributes>();
			PlayerAttributesPlayerFifaApi = new HashSet<PlayerAttributes>();
		}

		public int Id { get; set; }
		public int? PlayerApiId { get; set; }
		public string PlayerName { get; set; }
		public int? PlayerFifaApiId { get; set; }
		public DateTime? Birthday { get; set; }
		public int? Height { get; set; }
		public int? Weight { get; set; }

		public virtual ICollection<Match> MatchAwayPlayer10Navigation { get; set; }
		public virtual ICollection<Match> MatchAwayPlayer11Navigation { get; set; }
		public virtual ICollection<Match> MatchAwayPlayer1Navigation { get; set; }
		public virtual ICollection<Match> MatchAwayPlayer2Navigation { get; set; }
		public virtual ICollection<Match> MatchAwayPlayer3Navigation { get; set; }
		public virtual ICollection<Match> MatchAwayPlayer4Navigation { get; set; }
		public virtual ICollection<Match> MatchAwayPlayer5Navigation { get; set; }
		public virtual ICollection<Match> MatchAwayPlayer6Navigation { get; set; }
		public virtual ICollection<Match> MatchAwayPlayer7Navigation { get; set; }
		public virtual ICollection<Match> MatchAwayPlayer8Navigation { get; set; }
		public virtual ICollection<Match> MatchAwayPlayer9Navigation { get; set; }
		public virtual ICollection<Match> MatchHomePlayer10Navigation { get; set; }
		public virtual ICollection<Match> MatchHomePlayer11Navigation { get; set; }
		public virtual ICollection<Match> MatchHomePlayer1Navigation { get; set; }
		public virtual ICollection<Match> MatchHomePlayer2Navigation { get; set; }
		public virtual ICollection<Match> MatchHomePlayer3Navigation { get; set; }
		public virtual ICollection<Match> MatchHomePlayer4Navigation { get; set; }
		public virtual ICollection<Match> MatchHomePlayer5Navigation { get; set; }
		public virtual ICollection<Match> MatchHomePlayer6Navigation { get; set; }
		public virtual ICollection<Match> MatchHomePlayer7Navigation { get; set; }
		public virtual ICollection<Match> MatchHomePlayer8Navigation { get; set; }
		public virtual ICollection<Match> MatchHomePlayer9Navigation { get; set; }
		public virtual ICollection<PlayerAttributes> PlayerAttributesPlayerApi { get; set; }
		public virtual ICollection<PlayerAttributes> PlayerAttributesPlayerFifaApi { get; set; }
	}
}