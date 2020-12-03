using System;
using System.Collections.Generic;

namespace SoccerDataImporter.DatabaseModels
{
    public partial class Country
    {
        public Country()
        {
            	League = new HashSet<League>();
            	Match = new HashSet<Match>();
		EloRating = new HashSet<EloRating>();
	}

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<League> League { get; set; }
        public virtual ICollection<Match> Match { get; set; }
        public virtual ICollection<EloRating> EloRating { get; set; }
	}
}
