using Polly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientHelper
{
    public class HttpCaller : IHttpCaller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpCaller(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private int retryCount = 0;

        public Task<HttpResponseMessage> CallFailApi()
        {
            var policy = Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.InternalServerError)
                .WaitAndRetry(
                    5,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, context) =>
                    {
                        retryCount++;
                        Console.WriteLine();
                        Console.WriteLine($"Correlation Id {context.CorrelationId} has been retried {retryCount} times");
                        Console.WriteLine();
                    });

            var _client = _httpClientFactory.CreateClient();

            policy.Execute(() =>
            {
                var response = _client.GetAsync("https://localhost:5001/api/fail");
                return response.Result;
            });

            return null;
        }
    }
}