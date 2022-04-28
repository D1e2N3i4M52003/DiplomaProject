using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;
using WebAPI.Authorization;
using WebAPI.Helpers;
using Business.JSONModels;
using DataLayer.Models;

namespace Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IAuthenticate _authenticate;
        private readonly IExcursionService _excursionService;
        public IJwtUtils _jwt { get; set; }

        public UsersController(IUserService service, IAuthenticate authenticate, IExcursionService excursionService)
        {
            _service = service;
            _authenticate = authenticate;
            _excursionService = excursionService;
        }



        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {

            AuthenticateResponse response = await _authenticate.AuthenticateUser(model);
            return Ok(response);
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            return  Ok(await _service.GetAll());
        }


        [AllowAnonymous]
        [HttpPost("{id}")]
        public async Task<IActionResult> GetById(RequestUser id)
        {
            // only admins can access other user records
            Guid Id = id.Id;
            var user = await _service.GetById(Id);
            return Ok(user);
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async ValueTask<IActionResult> Create(CreateUserRequest model)
        {    
            try
            {
                await _service.CreateAsync(model);
                AuthenticateRequest request = new AuthenticateRequest()
                {
                    Password = model.Password,
                    Email = model.Email,
                };
                AuthenticateResponse response = await _authenticate.AuthenticateUser(request);
                return CreatedAtAction("create", response);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }
        
        [Authorize(Role.Admin)]
        [HttpPatch("[action]")]
        public async ValueTask<IActionResult> Edit(EditUserRequest model)
        {
                await _service.EditAsync(model);
                return Ok(_service.GetByAsync(d => d.Email == model.Email));
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpPatch("[action]")]
        public async ValueTask<IActionResult> ChangePassword(ChangePasswordRequest model)
        {
            try
            {
                await _service.ChangePassword(model.UserId, model);
                return Ok(new JSONMessage("Password updated successfully!"));
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [Authorize(Role.Admin)]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(UserModel model)
        {
            if (model.Id == model.Id)
                return Conflict(new JSONMessage("Cannot delete admin profile!"));

            await _service.DeleteAsync(model.Id);
            return Ok(new JSONMessage("User deleted successfully!"));
        }
        [Authorize(Role.Admin, Role.Moderator, Role.User)]
        [HttpPatch("[action]")]
        public async Task<IActionResult> Reserve(ReserveModel model)
        {

            await _service.ReserveExcursion(model.UserId, model.ExcursionId);
            return Ok(new JSONMessage("Excursion reserved successfully!"));
        }
    }
}
