using Business.JSONModels;
using DataLayer.Interfaces;
using DataLayer.Models;
using Business.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IUserRepository _userRepository;

        public PostService(IPostRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }
        
        public async Task<List<PostModel>> GetAll()
        {
            List<Post> posts = _repository.GetAll().Result.ToList();
            List<PostModel> postsModel = new List<PostModel>();
            foreach (var post in posts)
            {
                PostModel postModel = new PostModel
                {

                    Id = post.Id,
                    Title = post.Title,
                    Text = post.Text,
                    PostDate = post.PostDate,
                    AuthorId = post.AuthorId
                };

                postsModel.Add(postModel);
            };
            return postsModel;
        }

        public async Task<List<PostModel>> GetAll(Expression<Func<Post, bool>> filter)
        {
            ICollection<Post> posts = await _repository.GetAll(filter);
            List<PostModel> postsModel = new List<PostModel>();
            foreach (var post in posts)
            {
                PostModel postModel = new PostModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Text = post.Text,
                    PostDate = post.PostDate
                };

                postsModel.Add(postModel);
            };
            return postsModel;
        }

        public async Task<PostModel> GetById(Guid id)
        {
            Post? post = await _repository.GetByIdAsync(id);
            if (post == null) throw new KeyNotFoundException("Post not found");
            PostModel postModel = new PostModel
            {
                Title = post.Title,
                Text = post.Text,
                PostDate = post.PostDate
            };
            return postModel;
        }

        public async Task<PostModel> GetByAsync(Expression<Func<Post, bool>> filter)
        {
            ICollection<Post?> posts = await _repository.GetByAsync(filter);
            if (posts.Count==0) throw new KeyNotFoundException("Post not found");
            Post post = posts.First();
            PostModel postModel = new PostModel
            {
                Text = post.Text,
                PostDate = post.PostDate
            };
            return postModel;
        }

        public async Task CreateAsync(PostModel model)
        {
            User author = await _userRepository.GetByIdAsync(model.AuthorId);

            if (author == null) throw new KeyNotFoundException("User not found");

            Post post = new()
            {
                Id = Guid.NewGuid(),
                Author = author,
                Text = model.Text,
                PostDate = DateTime.Now,
                Title = model.Title,
                AuthorId = model.AuthorId
            };
            //author.Posts.Add(post);

            await _repository.CreateAsync(post);

        }

        public async Task EditAsync(PostModel model)
        {
            Post post = await _repository.GetByIdAsync(model.Id);

            if (post == null) throw new KeyNotFoundException("Post not found");

            post.Text = model.Text;
            post.Title = model.Title;
            await _repository.UpdateAsync(post);

        }

        public async Task DeleteAsync(Guid id)
        {

            Post post = await _repository.GetByIdAsync(id);

            if (post == null) throw new KeyNotFoundException("Post not found");
            await _repository.DeleteAsync(id);
        }
    }
}
