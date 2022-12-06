using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;

namespace api
{
    public class GetConversationToken
    {
        private readonly ILogger _logger;

        // HttpClient lifecycle management best practices:
        // https://learn.microsoft.com/dotnet/fundamentals/networking/http/httpclient-guidelines#recommended-use
        private static HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://directline.botframework.com/v3/directline/tokens/"),
        };

        public GetConversationToken(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetConversationToken>();
        }

        [Function("GetConversationToken")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // Authenticate using secret
            var secret = Environment.GetEnvironmentVariable("DirectLineSecret");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", secret);

            using HttpResponseMessage ApiResponse = await httpClient.PostAsync("generate", null);
            ApiResponse.EnsureSuccessStatusCode();
            var jsonResponse = await ApiResponse.Content.ReadAsStringAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/json; charset=utf-8");
            response.WriteString(jsonResponse);

            return response;
        }
    }
}
