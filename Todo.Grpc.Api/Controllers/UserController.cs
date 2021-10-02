using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.Grpc.Api.Extensions;
using Todo.Grpc.Api.Models;
using Todo.Grpc.Api.Shared.Enums;
using Todo.Grpc.Api.Shared.Interfaces;

namespace Todo.Grpc.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// GET api/user/{name}
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        [EnableCors("CorsPolicy")]
        public IActionResult OnGet(string name)
        {
            return Json(new {name});
        }

        /// <summary>
        /// POST api/user/auth
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeByRole(ERolePolicy.Admin, ERolePolicy.User)]
        public IActionResult Auth([FromBody] PersonDto person)
        {
            return Json(new { name = person.Name });
        }
    }
}