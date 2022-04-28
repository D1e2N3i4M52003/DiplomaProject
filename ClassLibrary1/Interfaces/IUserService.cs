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
        Task<List<UserModel>> GetAll();
        Task<List<UserModel>> GetAll(Expression<Func<User, bool>> filter);
        Task<EditUserRequest> GetById(Guid id);
        Task<UserModel> GetByAsync(Expression<Func<User, bool>> filter);
        ValueTask CreateAsync(CreateUserRequest model);
        ValueTask EditAsync(EditUserRequest model);
        ValueTask ChangePassword(Guid id, ChangePasswordRequest model);
        Task DeleteAsync(Guid id);
        Task ReserveExcursion(Guid userId, Guid excursionId);

    }
}
