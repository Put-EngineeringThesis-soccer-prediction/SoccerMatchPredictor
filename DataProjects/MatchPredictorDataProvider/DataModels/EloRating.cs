using Newtonsoft.Json;
using System;

namespace SoccerDataImporter.DatabaseModels
{
	public partial class EloRating
	{
		public int Id { get; set; }
		public string Rank { get; set; }
		public int TeamApiId { get; set; }
		public int CountryId { get; set; }
		public int Level { get; set; }
		public decimal Elo { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		[JsonIgnore]
		public virtual Team TeamApi { get; set; }

		public virtual Country Country { get; set; }
	}
}