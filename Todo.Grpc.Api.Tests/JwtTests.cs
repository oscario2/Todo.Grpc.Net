using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Todo.Grpc.FireBase.Request;
using Todo.Grpc.Shared.Services;
using Xunit;

namespace Todo.Grpc.Api.Tests
{
    public class UnitTest1
    {
        // ReSharper disable StringLiteralTypo
        private readonly string _apiKey = "AIzaSyDld9K_RaezUr9JDGftvA50JzBnAUIlL-Q";

        /// <summary>
        /// https://firebase.google.com/docs/reference/rest/auth#section-sign-in-email-password
        /// </summary>
        [Fact]
        public async Task RequestJwt_Success()
        {
            // arrange
            var login = new FireBaseLoginPasswordRequest("test@test.com", "mytest");

            // act
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json"),
                RequestUri =
                    new Uri($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={_apiKey}"),
            };
            var res = await HttpService.Request(req);

            // assert
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }

        [Fact]
        public async Task RequestJwt_UserNotFound()
        {
            // arrange
            var login = new FireBaseLoginPasswordRequest("invalid", "invalid");
            
            // act
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json"),
                RequestUri =
                    new Uri($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={_apiKey}"),
            };
            var res = await HttpService.Request(req);
            
            // assert
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);

        }
    }
}