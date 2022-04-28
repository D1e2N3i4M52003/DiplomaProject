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
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetAll();
            return Ok(posts);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var post = await _postService.GetById(id);
            return Ok(post);
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpPost("[action]")]
        public async ValueTask<IActionResult> Create(PostModel model)
        { 
                _postService.CreateAsync(model);
                PostModel post =  await _postService.GetByAsync(d => d.Title == model.Title);
                return CreatedAtAction("create", post);
        
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpPatch("[action]")]
        public async ValueTask<IActionResult> Edit(PostModel model)
        {

                _postService.EditAsync(model);
                PostModel post = await _postService.GetByAsync(d => d.PostDate == model.PostDate);
                return Ok(post);
          
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(PostModel model)
        {
            await _postService.DeleteAsync(model.Id);
            return Ok(new JSONMessage("Post deleted successfully!"));
        }
    }
}
