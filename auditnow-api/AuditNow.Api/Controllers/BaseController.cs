#region Using
using AuditNow.Core.Models.ValueObjects;
using AuditNow.Core.Models;
using AuditNow.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
#endregion

namespace AuditNow.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {


        public readonly IUserService _userService;
        protected User requestUser;


        public BaseController(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            this._userService = userService;

            var identity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                if(identity.FindFirst("UserId") != null)
                {
                    int userId = Convert.ToInt32(identity.FindFirst("UserId").Value);
                    ReturnObject<User> ret = _userService.GetUserById(userId, true);
                    if (ret.IsSuccessful == true)
                    {
                        requestUser = ret.Data.First();
                    }
                }
            }
        }


    }
}