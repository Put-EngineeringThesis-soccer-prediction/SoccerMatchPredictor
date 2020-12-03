using System;
using System.Collections.Generic;

namespace SoccerDataImporter.DatabaseModels
{
    public partial class PremierLeaguePlayersAttributes
    {
        public int Id { get; set; }
        public int? PlayerFifaApiId { get; set; }
        public int? PlayerApiId { get; set; }
        public DateTime? Date { get; set; }
        public int? OverallRating { get; set; }
        public int? Potential { get; set; }
        public string PreferredFoot { get; set; }
        public string AttackingWorkRate { get; set; }
        public string DefensiveWorkRate { get; set; }
        public int? Crossing { get; set; }
        public int? Finishing { get; set; }
        public int? HeadingAccuracy { get; set; }
        public int? ShortPassing { get; set; }
        public int? Volleys { get; set; }
        public int? Dribbling { get; set; }
        public int? Curve { get; set; }
        public int? FreeKickAccuracy { get; set; }
        public int? LongPassing { get; set; }
        public int? BallControl { get; set; }
        public int? Acceleration { get; set; }
        public int? SprintSpeed { get; set; }
        public int? Agility { get; set; }
        public int? Reactions { get; set; }
        public int? Balance { get; set; }
        public int? ShotPower { get; set; }
        public int? Jumping { get; set; }
        public int? Stamina { get; set; }
        public int? Strength { get; set; }
        public int? LongShots { get; set; }
        public int? Aggression { get; set; }
        public int? Interceptions { get; set; }
        public int? Positioning { get; set; }
        public int? Vision { get; set; }
        public int? Penalties { get; set; }
        public int? Marking { get; set; }
        public int? StandingTackle { get; set; }
        public int? SlidingTackle { get; set; }
        public int? GkDiving { get; set; }
        public int? GkHandling { get; set; }
        public int? GkKicking { get; set; }
        public int? GkPositioning { get; set; }
        public int? GkReflexes { get; set; }
    }
}
