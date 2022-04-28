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
    public interface IPostService
    {
        Task<PostModel> GetById(Guid id);
        Task<PostModel> GetByAsync(Expression<Func<Post, bool>> filter);
        Task CreateAsync(PostModel model);
        Task EditAsync(PostModel model);
        Task DeleteAsync(Guid id);
        Task<List<PostModel>> GetAll();
        Task<List<PostModel>> GetAll(Expression<Func<Post, bool>> filter);
    }
}
