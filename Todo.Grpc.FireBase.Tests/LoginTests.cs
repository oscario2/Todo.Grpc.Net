using System.Threading.Tasks;
using Todo.Grpc.FireBase.Enums;
using Todo.Grpc.FireBase.Extensions;
using Todo.Grpc.FireBase.Request;
using Xunit;
// ReSharper disable StringLiteralTypo

namespace Todo.Grpc.FireBase.Tests
{
    public class LoginTests
    {
        [Fact]
        public async Task PasswordLogin_Success()
        {
            // arrange
            var req = new FireBaseLoginPasswordRequest("test@test.com", "mytest");
            
            // act
            var res = await req.Dispatch();

            // assert
            Assert.False(res.IsError);
        }

        [Fact]
        public async Task PasswordLogin_Fail()
        {
            // arrange
            var req = new FireBaseLoginPasswordRequest("test@test.com", "invalid");
            
            // act
            var res = await req.Dispatch();

            // assert
            Assert.True(res.IsError);
            Assert.Equal(res.Error()?.Error.Message, EStatusCode.InvalidPassword.GetDisplayAttributes()?.Name);
        }
    }
}