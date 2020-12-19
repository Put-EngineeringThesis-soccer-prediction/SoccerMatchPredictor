using SoccerDataImporter.Models;
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

		public virtual Team TeamApi { get; set; }
		public virtual Country Country { get; set; }

		public static EloRating GetDbFromEloRating(EloRatingApiModel apiModel,
			Team relevantTeam,
			Country relevantCountry,
			int id)
		{
			return new EloRating()
			{
				Rank = apiModel.Rank,
				Level = apiModel.Level,
				TeamApiId = relevantTeam.TeamApiId.Value,
				CountryId = relevantCountry.Id,
				Elo = Convert.ToDecimal(apiModel.Elo),
				StartDate = apiModel.From,
				EndDate = apiModel.To
			};
		}
	}
}