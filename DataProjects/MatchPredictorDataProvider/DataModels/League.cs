using System;
using System.Collections.Generic;

namespace SoccerDataImporter.DatabaseModels
{
    public partial class League
    {
        public League()
        {
            Match = new HashSet<Match>();
        }

        public int Id { get; set; }
        public int? CountryId { get; set; }
        public string Name { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Match> Match { get; set; }
    }
}
