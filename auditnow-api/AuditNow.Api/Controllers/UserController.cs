#region Using
using AuditNow.Api.Resources.User;
using AuditNow.Core.Models.ValueObjects;
using AuditNow.Core.Models;
using AuditNow.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuditNow.Api.Validators.User;
#endregion

namespace AuditNow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;


        public UserController(IHttpContextAccessor context, IUserService userService, IMapper mapper) : base(context, userService)
        {
            _mapper = mapper;
        }


        [HttpGet("id/{userId}")]
        public async Task<ActionResult<ReturnObject<UserResource>>> GetUserById(int userId)
        {
            if (requestUser == null) return Unauthorized();

            try
            {
                ReturnObject<UserResource> response = new ReturnObject<UserResource>();

                ReturnObject<User> ret = _userService.GetUserById(userId, null);
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


        [HttpPost("")]
        public async Task<ActionResult<ReturnObject<UserResource>>> CreateUser([FromBody] CreateUserResource saveUserResource)
        {
            if (requestUser == null) return Unauthorized();

            var validationResult = await new CreateUserResourceValidator().ValidateAsync(saveUserResource);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            try
            {
                ReturnObject<UserResource> response = new ReturnObject<UserResource>();

                var userToCreate = _mapper.Map<CreateUserResource, User>(saveUserResource);
                userToCreate.CreationUserId = requestUser.UserId;
                userToCreate.CreationDate = DateTime.UtcNow;
                userToCreate.ModificationUserId = requestUser.UserId;
                userToCreate.ModificationDate = DateTime.UtcNow;
                userToCreate.IsActive = true;

                ReturnObject<User> ret = await _userService.CreateUser(userToCreate);
                if (ret.IsSuccessful == true)
                {
                    ReturnObject<User> createdUser = _userService.GetUserById(ret.Data.First().UserId, true);
                    UserResource userResource = _mapper.Map<User, UserResource>(createdUser.Data.First());
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
