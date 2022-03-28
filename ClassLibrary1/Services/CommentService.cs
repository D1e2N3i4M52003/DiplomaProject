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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IUserRepository _userRepository;

        public CommentService(ICommentRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }
        
        public async Task<List<CommentModel>> GetAll()
        {
            List<Comment> comments = await _repository.GetAll().Select(d => d).ToListAsync();
            List<CommentModel> commentsModel = new List<CommentModel>();
            foreach (var comment in comments)
            {
                CommentModel commentModel = new CommentModel
                {
                    AuthorUsername = comment.Author.Username,
                    Text = comment.Text,
                    PostDate = comment.PostDate
                };

                commentsModel.Add(commentModel);
            };
            return commentsModel;
        }

        public async Task<List<CommentModel>> GetAll(Expression<Func<Comment, bool>> filter)
        {
            List<Comment> comments = await _repository.GetAll(filter).Select(d => d).ToListAsync();
            List<CommentModel> commentsModel = new List<CommentModel>();
            foreach (var comment in comments)
            {
                CommentModel commentModel = new CommentModel
                {
                    AuthorUsername = comment.Author.Username,
                    Text = comment.Text,
                    PostDate = comment.PostDate
                };

                commentsModel.Add(commentModel);
            };
            return commentsModel;
        }

        public async Task<CommentModel> GetById(Guid id)
        {
            Comment? comment = await _repository.GetByIdAsync(id);
            if (comment == null) throw new KeyNotFoundException("Comment not found");
            CommentModel commentModel = new CommentModel
            {
                AuthorUsername = comment.Author.Username,
                Text = comment.Text,
                PostDate = comment.PostDate
            };
            return commentModel;
        }

        public async Task<CommentModel> GetByAsync(Expression<Func<Comment, bool>> filter)
        {
            Comment? comment = await _repository.GetByAsync(filter);
            if (comment == null) throw new KeyNotFoundException("Comment not found");
            CommentModel commentModel = new CommentModel
            {
                AuthorUsername = comment.Author.Username,
                Text = comment.Text,
                PostDate = comment.PostDate
            };
            return commentModel;
        }

        public async Task CreateAsync(CommentModel model)
        {
            User author = await _userRepository.GetByIdAsync(model.AuthorId);

            if (author == null) throw new KeyNotFoundException("User not found");

            Comment comment = new()
            {
                Id = Guid.NewGuid(),
                Author = author,
                Text = model.Text,
                PostDate = DateTime.Now
            };
            author.Comments.Add(comment);

            await _repository.CreateAsync(comment);

        }

        public async Task EditAsync(CommentModel model)
        {
            Comment comment = await _repository.GetByIdAsync(model.Id);

            if (comment == null) throw new KeyNotFoundException("Comment not found");

            comment.Text = model.Text;

            await _repository.UpdateAsync(comment);

        }

        public async Task DeleteAsync(Guid id)
        {

            Comment comment = await _repository.GetByIdAsync(id);

            if (comment == null) throw new KeyNotFoundException("Comment not found");
            await _repository.DeleteAsync(id);
        }
        /*
        public Task Like(UserModel model, CommentModel cModel)
        {
            Like like = new Lke
            {
                PostId = cModel.Id,
                UserId = model.Id
            };
        }*/
    }
}
