using Business.JSONModels;
using DataLayer.Models;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        Task<List<UserModel>> GetAll();
        Task<List<UserModel>> GetAll(Expression<Func<User, bool>> filter);
        Task<UserModel> GetById(Guid id);
        Task<UserModel> GetByAsync(Expression<Func<User, bool>> filter);
        Task CreateAsync(CreateUserRequest model);
        Task EditAsync(EditUserRequest model);
        Task ChangePassword(Guid id, ChangePasswordRequest model);
        Task DeleteAsync(Guid id);
        Task ReserveExcursion(ExcursionModel model, Guid id);

    }
}
