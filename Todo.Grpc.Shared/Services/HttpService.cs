using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Todo.Grpc.Shared.Interfaces;

namespace Todo.Grpc.Shared.Services
{
    public class CustomHttpResponse
    {
        public HttpResponseHeaders? Headers;
        public HttpStatusCode StatusCode;
        public string? ContentString;
        public byte[]? ContentBytes;
    }

    public static class HttpService
    {
        private static HttpClient? _client;

        public static async Task<T> GetJson<T>(string url)
            where T : IDataTransferObject
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };
            var res = await Request(req);

            return JsonConvert.DeserializeObject<T>(res.ContentString);
        }

        public static async Task<T> PostJson<T>(string url, string body)
            where T : IDataTransferObject
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                Content = new StringContent(body, Encoding.UTF8, "application/json"),
                RequestUri = new Uri(url),
            };
            var res = await Request(req);

            return JsonConvert.DeserializeObject<T>(res.ContentString);
        }

        public static async Task<CustomHttpResponse> Request(HttpRequestMessage req)
        {
            var ctx = GetClient();
            var res = ctx.SendAsync(req);

            var result = res.Result;
            var contentString = await result.Content.ReadAsStringAsync();
            var contentBytes = await result.Content.ReadAsByteArrayAsync();

            return new CustomHttpResponse()
            {
                Headers = result.Headers,
                StatusCode = res.Result.StatusCode,
                ContentString = contentString,
                ContentBytes = contentBytes
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