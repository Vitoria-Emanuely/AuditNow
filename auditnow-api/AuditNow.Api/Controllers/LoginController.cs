using AuditNow.Api.Resources.User;
using AuditNow.Core.Models.ValueObjects;
using AuditNow.Core.Models;
using AuditNow.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuditNow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;


        public LoginController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        [HttpPost("")]
        public async Task<ActionResult<UserResource>> Login([FromBody] LoginResource loginResource)
        {
            try
            {
                ReturnObject<UserResource> response = new ReturnObject<UserResource>();

                ReturnObject<User> ret = await _userService.Login(loginResource.Email, loginResource.Password);
                if (ret.IsSuccessful == true)
                {
                    UserResource userResource = _mapper.Map<User, UserResource>(ret.Data.First());
                    response.Data = new List<UserResource> { userResource };
                }

                response.IsSuccessful = ret.IsSuccessful;
                response.Message = ret.Message;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
