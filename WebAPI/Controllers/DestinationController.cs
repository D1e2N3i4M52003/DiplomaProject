using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;
using WebAPI.Authorization;
using WebAPI.Helpers;
using Business.JSONModels;
using DataLayer.Models;
namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DestinationController : ControllerBase
    {
        private readonly IDestinationService _destinationService;

        public DestinationController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var destinations = await _destinationService.GetAll();
            return Ok(destinations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var destinations = await _destinationService.GetById(id);
            return Ok(destinations);
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpPost("[action]")]
        public async ValueTask<IActionResult> Create(DestinationModel model)
        {
            await _destinationService.CreateAsync(model);
            DestinationModel destination = await _destinationService.GetByAsync(d=> d.Name==model.Name);
            return CreatedAtAction("create", destination); 
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpPatch("[action]")]
        public async ValueTask<IActionResult> Edit(DestinationModel model)
        {
                await _destinationService.EditAsync(model);
                DestinationModel destination = await _destinationService.GetById(model.Id);
                return CreatedAtAction("create", destination);
           
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(DestinationModel model)
        {
            await _destinationService.DeleteAsync(model.Id);
            return Ok(new JSONMessage("Post deleted successfully!"));
        }
    }
}
