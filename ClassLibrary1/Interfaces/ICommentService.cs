using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using Business.JSONModels;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface ICommentService
    {
        Task<CommentModel> GetById(Guid id);
        Task<CommentModel> GetByAsync(Expression<Func<Comment, bool>> filter);
        Task CreateAsync(CommentModel model);
        Task EditAsync(CommentModel model);
        Task DeleteAsync(Guid id);
        Task<List<CommentModel>> GetAll();
        Task<List<CommentModel>> GetAll(Expression<Func<Comment, bool>> filter);
    }
}
