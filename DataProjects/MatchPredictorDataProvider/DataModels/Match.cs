using System;
using Newtonsoft.Json;

namespace SoccerDataImporter.DatabaseModels
{
	public partial class Match
	{
		public int Id { get; set; }
		public int? CountryId { get; set; }
		public int? LeagueId { get; set; }
		public string Season { get; set; }
		public int? Stage { get; set; }
		public DateTime? Date { get; set; }
		public int? MatchApiId { get; set; }
		public int? HomeTeamApiId { get; set; }
		public int? AwayTeamApiId { get; set; }
		public int? HomeTeamGoal { get; set; }
		public int? AwayTeamGoal { get; set; }
		public int? HomePlayerX1 { get; set; }
		public int? HomePlayerX2 { get; set; }
		public int? HomePlayerX3 { get; set; }
		public int? HomePlayerX4 { get; set; }
		public int? HomePlayerX5 { get; set; }
		public int? HomePlayerX6 { get; set; }
		public int? HomePlayerX7 { get; set; }
		public int? HomePlayerX8 { get; set; }
		public int? HomePlayerX9 { get; set; }
		public int? HomePlayerX10 { get; set; }
		public int? HomePlayerX11 { get; set; }
		public int? AwayPlayerX1 { get; set; }
		public int? AwayPlayerX2 { get; set; }
		public int? AwayPlayerX3 { get; set; }
		public int? AwayPlayerX4 { get; set; }
		public int? AwayPlayerX5 { get; set; }
		public int? AwayPlayerX6 { get; set; }
		public int? AwayPlayerX7 { get; set; }
		public int? AwayPlayerX8 { get; set; }
		public int? AwayPlayerX9 { get; set; }
		public int? AwayPlayerX10 { get; set; }
		public int? AwayPlayerX11 { get; set; }
		public int? HomePlayerY1 { get; set; }
		public int? HomePlayerY2 { get; set; }
		public int? HomePlayerY3 { get; set; }
		public int? HomePlayerY4 { get; set; }
		public int? HomePlayerY5 { get; set; }
		public int? HomePlayerY6 { get; set; }
		public int? HomePlayerY7 { get; set; }
		public int? HomePlayerY8 { get; set; }
		public int? HomePlayerY9 { get; set; }
		public int? HomePlayerY10 { get; set; }
		public int? HomePlayerY11 { get; set; }
		public int? AwayPlayerY1 { get; set; }
		public int? AwayPlayerY2 { get; set; }
		public int? AwayPlayerY3 { get; set; }
		public int? AwayPlayerY4 { get; set; }
		public int? AwayPlayerY5 { get; set; }
		public int? AwayPlayerY6 { get; set; }
		public int? AwayPlayerY7 { get; set; }
		public int? AwayPlayerY8 { get; set; }
		public int? AwayPlayerY9 { get; set; }
		public int? AwayPlayerY10 { get; set; }
		public int? AwayPlayerY11 { get; set; }
		public int? HomePlayer1 { get; set; }
		public int? HomePlayer2 { get; set; }
		public int? HomePlayer3 { get; set; }
		public int? HomePlayer4 { get; set; }
		public int? HomePlayer5 { get; set; }
		public int? HomePlayer6 { get; set; }
		public int? HomePlayer7 { get; set; }
		public int? HomePlayer8 { get; set; }
		public int? HomePlayer9 { get; set; }
		public int? HomePlayer10 { get; set; }
		public int? HomePlayer11 { get; set; }
		public int? AwayPlayer1 { get; set; }
		public int? AwayPlayer2 { get; set; }
		public int? AwayPlayer3 { get; set; }
		public int? AwayPlayer4 { get; set; }
		public int? AwayPlayer5 { get; set; }
		public int? AwayPlayer6 { get; set; }
		public int? AwayPlayer7 { get; set; }
		public int? AwayPlayer8 { get; set; }
		public int? AwayPlayer9 { get; set; }
		public int? AwayPlayer10 { get; set; }
		public int? AwayPlayer11 { get; set; }
		public int? HomeTeamShots { get; set; }
		public int? AwayTeamShots { get; set; }
		public int? HomeTeamShotsOnTarget { get; set; }
		public int? AwayTeamShotsOnTarget { get; set; }
		public int? HomeTeamCorners { get; set; }
		public int? AwayTeamCorners { get; set; }
		public int? HomeTeamFoulsCommitted { get; set; }
		public int? AwayTeamFoulsCommitted { get; set; }
		public int? HomeTeamYellowCards { get; set; }
		public int? AwayTeamYellowCards { get; set; }
		public int? AwayTeamRedCards { get; set; }
		public int? HomeTeamRedCards { get; set; }
		public decimal? B365h { get; set; }
		public decimal? B365d { get; set; }
		public decimal? B365a { get; set; }
		public decimal? Bwh { get; set; }
		public decimal? Bwd { get; set; }
		public decimal? Bwa { get; set; }
		public decimal? Iwh { get; set; }
		public decimal? Iwd { get; set; }
		public decimal? Iwa { get; set; }
		public decimal? Lbh { get; set; }
		public decimal? Lbd { get; set; }
		public decimal? Lba { get; set; }
		public decimal? Psh { get; set; }
		public decimal? Psd { get; set; }
		public decimal? Psa { get; set; }
		public decimal? Whh { get; set; }
		public decimal? Whd { get; set; }
		public decimal? Wha { get; set; }
		public decimal? Sjh { get; set; }
		public decimal? Sjd { get; set; }
		public decimal? Sja { get; set; }
		public decimal? Vch { get; set; }
		public decimal? Vcd { get; set; }
		public decimal? Vca { get; set; }
		public decimal? Gbh { get; set; }
		public decimal? Gbd { get; set; }
		public decimal? Gba { get; set; }
		public decimal? Bsh { get; set; }
		public decimal? Bsd { get; set; }
		public decimal? Bsa { get; set; }

		[JsonIgnore]
		public virtual Player AwayPlayer10Navigation { get; set; }
		[JsonIgnore]
		public virtual Player AwayPlayer11Navigation { get; set; }
		[JsonIgnore]
		public virtual Player AwayPlayer1Navigation { get; set; }
		[JsonIgnore]
		public virtual Player AwayPlayer2Navigation { get; set; }
		[JsonIgnore]
		public virtual Player AwayPlayer3Navigation { get; set; }
		[JsonIgnore]
		public virtual Player AwayPlayer4Navigation { get; set; }
		[JsonIgnore]
		public virtual Player AwayPlayer5Navigation { get; set; }
		[JsonIgnore]
		public virtual Player AwayPlayer6Navigation { get; set; }
		[JsonIgnore]
		public virtual Player AwayPlayer7Navigation { get; set; }
		[JsonIgnore]
		public virtual Player AwayPlayer8Navigation { get; set; }
		[JsonIgnore]
		public virtual Player AwayPlayer9Navigation { get; set; }
		[JsonIgnore]
		public virtual Team AwayTeamApi { get; set; }
		[JsonIgnore]
		public virtual Country Country { get; set; }
		[JsonIgnore]
		public virtual Player HomePlayer10Navigation { get; set; }
		[JsonIgnore]
		public virtual Player HomePlayer11Navigation { get; set; }
		[JsonIgnore]
		public virtual Player HomePlayer1Navigation { get; set; }
		[JsonIgnore]
		public virtual Player HomePlayer2Navigation { get; set; }
		[JsonIgnore]
		public virtual Player HomePlayer3Navigation { get; set; }
		[JsonIgnore]
		public virtual Player HomePlayer4Navigation { get; set; }
		[JsonIgnore]
		public virtual Player HomePlayer5Navigation { get; set; }
		[JsonIgnore]
		public virtual Player HomePlayer6Navigation { get; set; }
		[JsonIgnore]
		public virtual Player HomePlayer7Navigation { get; set; }
		[JsonIgnore]
		public virtual Player HomePlayer8Navigation { get; set; }
		[JsonIgnore]
		public virtual Player HomePlayer9Navigation { get; set; }
		[JsonIgnore]
		public virtual Team HomeTeamApi { get; set; }
		[JsonIgnore]
		public virtual League League { get; set; }
	}
}