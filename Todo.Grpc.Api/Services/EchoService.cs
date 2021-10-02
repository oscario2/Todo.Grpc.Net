using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using Todo.Grpc.Api.Proto;

namespace Todo.Grpc.Api.Services
{
    public class EchoService : Echo.EchoBase
    {
        private readonly ILogger<EchoService> _logger;

        public EchoService(ILogger<EchoService> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        [EnableCors("AllowAll")]
        public override Task<EchoReply> GetEcho(EchoRequest request, ServerCallContext context)
        {
            return Task.FromResult(new EchoReply()
            {
                Message = request.Message
            });
        }
    }
}