using MatchPredictorDataProvider.Interfaces;
using MatchPredictorDataProvider.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoccerDataImporter.DatabaseModels;
using System;
using System.Configuration;

[assembly: FunctionsStartup(typeof(MatchPredictorDataProvider.Startup))]

namespace MatchPredictorDataProvider
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			var connectionString = Environment.GetEnvironmentVariable("SoccerMatchPredictDb");
			builder.Services.AddDbContext<MatchPredictDbContext>(options =>
				options.UseSqlServer(connectionString));

			builder.Services.AddTransient<IMatchPredictDbService, MatchPredictDbService>();
		}
	}
}