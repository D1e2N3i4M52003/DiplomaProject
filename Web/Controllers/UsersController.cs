using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;
using Business.Authorization;
using Business.Helpers;
using Business.JSONModels;
using DataLayer.Models;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _service.Authenticate(model);
            return Ok(response);
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            return  Ok(await _service.GetAll());
        }

        
        [Authorize(Role.Admin, Role.Moderator)]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            // only admins can access other user records
            var currentUser = (User)HttpContext.Items["User"];
            if (id != currentUser.Id && currentUser.Role != Role.Admin)
                return Unauthorized(new JSONMessage("Unauthorized"));

            var user = _service.GetById(id);
            return Ok(user);
        }

        [Authorize(Role.Admin)]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateUserRequest model)
        {
            try
            {
                await _service.CreateAsync(model);
                return CreatedAtAction("create", _service.GetByAsync( u => u.Email==  model.Email));
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpPatch("[action]")]
        public async Task<IActionResult> Edit(EditUserRequest model)
        {

            var currentUser = (User)HttpContext.Items["User"];
            if (model.ID != currentUser.Id && currentUser.Role != Role.Admin)
                return Unauthorized(new JSONMessage("Unauthorized"));

            try
            {
                
                await _service.EditAsync(model);
                return Ok(_service.GetById(model.ID));
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpPatch("[action]")]
        public IActionResult ChangePassword(ChangePasswordRequest model)
        {

            var currentUser = (User)HttpContext.Items["User"];

            try
            {
                _service.ChangePassword(currentUser.Id, model);
                return Ok(new JSONMessage("Password updated successfully!"));
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [Authorize(Role.Admin)]
        [HttpDelete("[action]/{id}")]
        public IActionResult Delete(Guid id)
        {
            var currentUser = (User)HttpContext.Items["User"];
            if (id == currentUser.Id && currentUser.Role == Role.Admin)
                return Conflict(new JSONMessage("Cannot delete admin profile!"));

            _service.DeleteAsync(id);
            return Ok(new JSONMessage("User deleted successfully!"));
        }
    }
}
