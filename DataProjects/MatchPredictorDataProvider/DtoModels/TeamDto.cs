﻿using SoccerDataImporter.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatchPredictorDataProvider.DtoModels
{
	public class TeamDto
	{
		public int Id { get; set; }
		public int TeamApiId { get; set; }
		public string TeamName { get; set; }
		public List<PlayerDto> Players { get; set; }
		public TeamAttributes Attributes { get; set; }
		public EloRating CurrentEloRating { get; set; }
	}
}
