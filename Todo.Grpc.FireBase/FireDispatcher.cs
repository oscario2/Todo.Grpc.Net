using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Todo.Grpc.FireBase.Interfaces;
using Todo.Grpc.FireBase.Response;
using Todo.Grpc.FireBase.Services;

namespace Todo.Grpc.FireBase
{
    public static class FireDispatcher
    {
        public static async Task<FireResult<T>> Dispatch<T>(string url, IFireRequest<T> req)
            where T : IFireResponse
        {
            var b = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                Content = new StringContent(req.Serialize(), Encoding.UTF8, "application/json"),
                RequestUri = new Uri(url),
            };
            var res = await HttpService.Request(b);
            
            var str = res.ContentString;
            if (str == null) return null!;

            return res.StatusCode == HttpStatusCode.OK
                ? FireResult<T>.Success(JsonConvert.DeserializeObject<T>(str))
                : FireResult<T>.Fail(JsonConvert.DeserializeObject<FireBaseError>(str));
        }
    }
}