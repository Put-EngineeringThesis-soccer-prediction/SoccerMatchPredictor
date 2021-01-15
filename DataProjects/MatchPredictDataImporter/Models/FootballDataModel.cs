using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MatchPredictDataImporter.Models
{
	public class FootballDataModel
	{
		public DateTime Date { get; set; }
		public string HomeTeam { get; set; }
		public string AwayTeam { get; set; }
		public int HomeTeamGoals { get; set; }
		public int AwayTeamGoals { get; set; }
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

		public static FootballDataModel FromCsv(string csvLine, List<string> headerList)
		{
			string[] values = csvLine.Split(',');

			FootballDataModel modelValues = new FootballDataModel();
			var dateString = values[headerList.IndexOf("Date")];
			if (dateString.Length < 9)
			{
				dateString = dateString.Insert(dateString.LastIndexOf('/') + 1, "20");
			}
			modelValues.Date = DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);

			modelValues.HomeTeam = values[headerList.IndexOf("HomeTeam")];
			var homeGoalsValue = headerList.FirstOrDefault(x => x == "FTHG" || x == "HG");
			modelValues.HomeTeamGoals = Convert.ToInt32(values[headerList.IndexOf(homeGoalsValue)]);
			modelValues.HomeTeamCorners = Convert.ToInt32(values[headerList.IndexOf("HC")]);
			modelValues.HomeTeamFoulsCommitted = Convert.ToInt32(values[headerList.IndexOf("HF")]);
			modelValues.HomeTeamRedCards = Convert.ToInt32(values[headerList.IndexOf("HR")]);
			modelValues.HomeTeamShots = Convert.ToInt32(values[headerList.IndexOf("HS")]);
			modelValues.HomeTeamShotsOnTarget = Convert.ToInt32(values[headerList.IndexOf("HST")]);
			modelValues.HomeTeamYellowCards = Convert.ToInt32(values[headerList.IndexOf("HY")]);

			modelValues.AwayTeam = values[headerList.IndexOf("AwayTeam")];
			var awaysGoalsValue = headerList.FirstOrDefault(x => x == "FTAG" || x == "AG");
			modelValues.AwayTeamGoals = Convert.ToInt32(values[headerList.IndexOf(awaysGoalsValue)]);
			modelValues.AwayTeamCorners = Convert.ToInt32(values[headerList.IndexOf("AC")]);
			modelValues.AwayTeamFoulsCommitted = Convert.ToInt32(values[headerList.IndexOf("AF")]);
			modelValues.AwayTeamRedCards = Convert.ToInt32(values[headerList.IndexOf("AR")]);
			modelValues.AwayTeamShots = Convert.ToInt32(values[headerList.IndexOf("AS")]);
			modelValues.AwayTeamShotsOnTarget = Convert.ToInt32(values[headerList.IndexOf("AST")]);
			modelValues.AwayTeamYellowCards = Convert.ToInt32(values[headerList.IndexOf("AY")]);

			return modelValues;
		}
	}
}