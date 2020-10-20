using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerDataImporter.Interfaces
{
	public interface IFootballDataImporter
	{
		Task ImportTeamFootballDataMissingValues(string sourceDirectory, Dictionary<string, string> footballToDbDict);
	}
}
