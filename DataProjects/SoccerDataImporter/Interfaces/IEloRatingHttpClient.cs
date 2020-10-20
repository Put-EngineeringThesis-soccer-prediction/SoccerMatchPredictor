﻿using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SoccerDataImporter.Interfaces
{
	public interface IEloRatingHttpClient
	{
		public Task<Stream> GetEloRatingResposne(Uri requestUri);
	}
}
