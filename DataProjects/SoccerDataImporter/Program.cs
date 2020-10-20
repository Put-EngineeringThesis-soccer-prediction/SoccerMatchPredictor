using SoccerDataImporter.Interfaces;
using SoccerDataImporter.DatabaseModels;
using SoccerDataImporter.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SoccerDataImporter.Models;

namespace SoccerDataImporter
{
	internal class Program
	{
		public static ServiceProvider _serviceProvider;

		public const string teamsToImportKey = "TeamsToImport";
		public const string teamsToMergeKey = "TeamsToMerge";
		public const string eloRatingDestinationDirKey = "DestinationDirectory";
		public const string modeKey = "ImportType";
		public const string footballDataDirKey = "SourceDirectory";

		public const string eloRatingMode = "EloRatingImport";
		public const string footballDataMergeMode = "KaggleFootballDataMerge";

		public static void Main(string[] args)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");

			var configuration = builder.Build();

			var destinationDirectory = configuration.GetValue<string>(eloRatingDestinationDirKey);
			var mode = configuration.GetValue<string>(modeKey);
			var services = new ServiceCollection();
			services.AddDbContext<MatchPredictDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("SoccerMatchPredictDb")));
			services.AddHttpClient<IEloRatingHttpClient, EloRatingHttpClient>();
			services.AddSingleton<IEloRatingImporter, EloRatingImporter>();
			services.AddSingleton<IFootballDataImporter, FootballDataImporter>();
			_serviceProvider = services
				.BuildServiceProvider();

			if (mode == eloRatingMode)
			{
				var teams = configuration.GetSection(teamsToImportKey).Get<List<TeamsToImportSetting>>();

				var importer = _serviceProvider.GetService<IEloRatingImporter>();
				foreach (var team in teams)
				{
					Task.Run(async () => await importer.ImportTeamEloRatingHistory(team, destinationDirectory)).GetAwaiter().GetResult();
				}
			}
			else if (mode == footballDataMergeMode)
			{
				var teams = configuration.GetSection(teamsToMergeKey).Get<List<TeamsToMergeSetting>>();
				Dictionary<string, string> footballToDbDict = new Dictionary<string, string>();
				foreach(var team in teams)
				{
					footballToDbDict[team.FootballDataTeamName] = team.DbTeamName;
				}
				var importer = _serviceProvider.GetService<IFootballDataImporter>();
				Task.Run(async () => await importer.ImportTeamFootballDataMissingValues(configuration.GetValue<string>(footballDataDirKey), footballToDbDict)).GetAwaiter().GetResult();
			}
		}
	}
}