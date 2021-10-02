using System.Threading.Tasks;
using Crypto.Web;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Todo.Grpc.Api.Extensions;
using Todo.Grpc.Api.Shared.Enums;

namespace Todo.Grpc.Api.Services
{
    [Authorize]
    public class AdminService : Admin.AdminBase
    {
        [AuthorizeByRole(ERolePolicy.Admin, ERolePolicy.User)]
        public override Task<GetStatusResponse> GetStatus(Empty request, ServerCallContext context)
        {
            // var isRole = context.GetHttpContext().User.IsInRole(EAuthRole.Admin.ToString());
            // var identity = context.GetHttpContext().User.Identity;

            return Task.FromResult(new GetStatusResponse()
            {
                Uptime = 100
            });
        }
    }
}