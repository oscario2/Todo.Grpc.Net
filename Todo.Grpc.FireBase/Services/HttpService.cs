using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Todo.Grpc.FireBase.Services
{
   public class CustomHttpResponse
    {
        public HttpResponseHeaders? Headers;
        public HttpStatusCode StatusCode;
        public string? ContentString;
    }

    public static class HttpService
    {
        private static HttpClient? _client;
        
        public static async Task<CustomHttpResponse> Request(HttpRequestMessage req)
        {
            var ctx = GetClient();
            var res = ctx.SendAsync(req);

            var result = res.Result;
            var contentString = await result.Content.ReadAsStringAsync();

            return new CustomHttpResponse()
            {
                Headers = result.Headers,
                StatusCode = res.Result.StatusCode,
                ContentString = contentString,
            };
        }

        private static HttpClient GetClient()
        {
            var clientHandler = new HttpClientHandler()
            {
                // for fiddler debug
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,

                // adds "Accept-Encoding: gzip, deflate, br" automatically
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            };
            _client = new HttpClient(clientHandler);

            return _client;
        }
    }
}