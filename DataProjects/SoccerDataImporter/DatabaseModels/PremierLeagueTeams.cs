namespace SoccerDataImporter.DatabaseModels
{
	public partial class PremierLeagueTeams
	{
		public int Id { get; set; }
		public int? TeamApiId { get; set; }
		public int? TeamFifaApiId { get; set; }
		public string TeamLongName { get; set; }
		public string TeamShortName { get; set; }
	}
}