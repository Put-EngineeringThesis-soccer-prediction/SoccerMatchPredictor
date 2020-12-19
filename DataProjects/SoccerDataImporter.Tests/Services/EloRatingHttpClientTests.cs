using Moq;
using Moq.Protected;
using NUnit.Framework;
using SoccerDataImporter.Interfaces;
using SoccerDataImporter.Services;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerDataImporter.Tests.Services
{
	[ExcludeFromCodeCoverage]
	[TestFixture]
	public class EloRatingHttpClientTests
	{
		private readonly Mock<HttpMessageHandler> handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

		private IEloRatingHttpClient eloRatingHttpClient;

		[SetUp]
		public void Setup()
		{
			handlerMock
				.Protected()
				.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(new HttpResponseMessage()
				{
					StatusCode = HttpStatusCode.OK,
					Content = new StringContent("[{'id':1,'value':'1'}]"),
				})
				.Verifiable();

			HttpClient httpClientMock = new HttpClient(handlerMock.Object);
			eloRatingHttpClient = new EloRatingHttpClient(httpClientMock);
		}

		[Test]
		public async Task EloRatingHttpClientShouldMakeSingleRequest()
		{
			var testUri = new Uri("https://someUri.com");
			await eloRatingHttpClient.GetEloRatingResposne(testUri);
			handlerMock.Protected().Verify("SendAsync", Times.Exactly(1),
				ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == testUri),
				ItExpr.IsAny<CancellationToken>());
		}
	}
}