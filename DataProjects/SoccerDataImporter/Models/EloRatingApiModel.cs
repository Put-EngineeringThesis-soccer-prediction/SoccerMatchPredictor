using System;
using System.Globalization;

namespace SoccerDataImporter.Models
{
	public class EloRatingApiModel
	{
		public string Rank { get; set; }
		public string Club { get; set; }
		public string Country { get; set; }
		public int Level { get; set; }
		public double Elo { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }

		public static EloRatingApiModel FromCsv(string csvLine)
		{
			string[] values = csvLine.Split(',');
			if (values.Length < 7)
			{
				return null;
			}
			EloRatingApiModel modelValues = new EloRatingApiModel();
			modelValues.Rank = values[0];
			modelValues.Club = values[1];
			modelValues.Country = values[2];
			modelValues.Level = Convert.ToInt32(values[3]);
			modelValues.Elo = Convert.ToDouble(values[4], CultureInfo.InvariantCulture);
			modelValues.From = Convert.ToDateTime(values[5]);
			modelValues.To = Convert.ToDateTime(values[6]);
			return modelValues;
		}
	}
}