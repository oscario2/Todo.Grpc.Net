using Microsoft.AspNetCore.Authorization;
using Todo.Grpc.Api.Shared.Enums;

namespace Todo.Grpc.Api.Extensions
{
    public class AuthorizeByRole : AuthorizeAttribute
    {
        public AuthorizeByRole(params ERolePolicy[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}