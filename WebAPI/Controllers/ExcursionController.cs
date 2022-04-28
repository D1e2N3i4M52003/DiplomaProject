using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;
using WebAPI.Authorization;
using WebAPI.Helpers;
using Business.JSONModels;
using DataLayer.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ExcursionController : ControllerBase
    {
        private readonly IExcursionService _excursionService;

        public ExcursionController(IExcursionService excursionService)
        {
            _excursionService = excursionService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var excursions = await _excursionService.GetAll();
            return Ok(excursions);
        }

        [AllowAnonymous]
        [HttpPost("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var excursions = await _excursionService.GetById(id);
            return Ok(excursions);
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpPost("[action]")]
        public async ValueTask<IActionResult> Create(ExcursionModel model)
        {
                await _excursionService.CreateAsync(model);
                ExcursionModel excursion = await _excursionService.GetByAsync(d => d.Name == model.Name);
                return CreatedAtAction("create", excursion);
          
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpPatch("[action]")]
        public async ValueTask<IActionResult> Edit(ExcursionModel model)
        {
                await _excursionService.EditAsync(model);
                ExcursionModel excursion = await _excursionService.GetById(model.Id);
                return Ok(excursion);
         }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(ExcursionModel model)
        {

            await _excursionService.DeleteAsync(model.Id);
            return Ok(new JSONMessage("Post deleted successfully!"));
        }
    }
}
