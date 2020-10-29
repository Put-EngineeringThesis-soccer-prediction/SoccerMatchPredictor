using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SoccerDataImporter.DatabaseModels
{
    public partial class TeamAttributes
    {
        public int Id { get; set; }
        public int? TeamFifaApiId { get; set; }
        public int? TeamApiId { get; set; }
        public DateTime? Date { get; set; }
        public int? BuildUpPlaySpeed { get; set; }
        public string BuildUpPlaySpeedClass { get; set; }
        public int? BuildUpPlayDribbling { get; set; }
        public string BuildUpPlayDribblingClass { get; set; }
        public int? BuildUpPlayPassing { get; set; }
        public string BuildUpPlayPassingClass { get; set; }
        public string BuildUpPlayPositioningClass { get; set; }
        public int? ChanceCreationPassing { get; set; }
        public string ChanceCreationPassingClass { get; set; }
        public int? ChanceCreationCrossing { get; set; }
        public string ChanceCreationCrossingClass { get; set; }
        public int? ChanceCreationShooting { get; set; }
        public string ChanceCreationShootingClass { get; set; }
        public string ChanceCreationPositioningClass { get; set; }
        public int? DefencePressure { get; set; }
        public string DefencePressureClass { get; set; }
        public int? DefenceAggression { get; set; }
        public string DefenceAggressionClass { get; set; }
        public int? DefenceTeamWidth { get; set; }
        public string DefenceTeamWidthClass { get; set; }
        public string DefenceDefenderLineClass { get; set; }

        [JsonIgnore]
        public virtual Team TeamApi { get; set; }
        [JsonIgnore]
        public virtual Team TeamFifaApi { get; set; }
    }
}
