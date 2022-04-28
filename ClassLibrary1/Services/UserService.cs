using Business.JSONModels;
using DataLayer.Models;
using Business.Interfaces;
using DataLayer.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IExcursionRepository _excursionRepository;
        private readonly IExcursionService _excursionService;

        public UserService(IUserRepository repository, IExcursionRepository excursionRepository, IExcursionService excursionService)
        {
            _repository = repository;
            _excursionRepository = excursionRepository;
            _excursionService = excursionService;
        }

        public async ValueTask CreateAsync(CreateUserRequest model)
        {
            User user = new User
            {
                Id = Guid.NewGuid(),
                Username = model.Username,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Email = model.Email,
                CreationDate = DateTime.Now,
                Role = (Role)Enum.Parse(typeof(Role), "User"),
                PasswordHash = BCryptNet.HashPassword(model.Password)
            };
            await _repository.CreateAsync(user);
        }

        public async ValueTask EditAsync(EditUserRequest model)
        {
            Enum.TryParse(model.UserRole, out Role userRole);
            User user = new User
            {
                Username = model.Username,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Email = model.Email,
                Role = userRole
            };

            await _repository.UpdateAsync(user);
        }

        public async Task<EditUserRequest> GetById(Guid id)
        {
            User? user = await _repository.GetByIdAsync(id);
            if (user is null)
            {
                throw new ArgumentException("No such user exists!");
            }
            EditUserRequest userModel = new EditUserRequest
            {
                Username = user.Username,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Excursions = new List<ExcursionModel>()
            };
            foreach(var excursion in user.Excursions)
            {
                ExcursionModel excursionModel = await _excursionService.GetById(excursion.Id);
                userModel.Excursions.Add(excursionModel);
            }
            return userModel;
        }

        public async Task<UserModel> GetByAsync(Expression<Func<User, bool>> filter)
        {
            ICollection<User?> users = await _repository.GetByAsync(filter);
            if (users.Count==0)
            {
                throw new ArgumentException("No such user exists!");
            }
            User user = users.First();
            UserModel userModel = new UserModel
            {
                Username = user.Username,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email
            };
            return userModel;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<UserModel>> GetAll()
        {
            List<User> users = _repository.GetAll().Result.ToList();
            List<UserModel> usersModel = new List<UserModel>();
            foreach (var user in users)
            {
                UserModel userModel = new UserModel
                {
                    Username = user.Username,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Email = user.Email
                };
                usersModel.Add(userModel);
            }
            return usersModel;
        }
         public async Task<List<UserModel>> GetAll(Expression<Func<User, bool>> filter)
        {
            ICollection<User> users = await _repository.GetAll(filter);
            List<UserModel> usersModel = new List<UserModel>();
            foreach (var user in users)
            {
                UserModel userModel = new UserModel
                {
                    Username = user.Username,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Email = user.Email
                };
                usersModel.Add(userModel);
            }
            return usersModel;
        }

        public async ValueTask ChangePassword(Guid id, ChangePasswordRequest model)
        {
            ICollection<User?> users = await _repository.GetByAsync(x => x.Id == id);
            if (users.Count == 0)
            {
                throw new ArgumentException("No such user exists!");
            }
            User user = users.First();
            try 
            {
                if (user.PasswordHash == BCryptNet.HashPassword(model.OldPassword))
                {
                    user.PasswordHash = BCryptNet.HashPassword(model.NewPassword);
                    await _repository.UpdateAsync(user);
                }
                else
                {
                    throw new ArgumentException("Wrong password!");
                }
            }
            catch (Exception)
            {
                throw new NullReferenceException();
            }
        }

        public async Task ReserveExcursion(Guid userId,Guid excursionId)
        {
            Excursion? excursion = await _excursionRepository.GetByIdAsync(excursionId);
            if (excursion is null)
            {
                throw new ArgumentException("No such excursion exists!");
            }
            User? user = await _repository.GetByIdAsync(userId);
            if (user is null)
            {
                throw new ArgumentException("No such user exists!");
            }
            if(user.Excursions==null)
            {
                user.Excursions = new List<Excursion>();
            }
            user.Excursions.Add(excursion);
            await _repository.UpdateAsync(user);
        }

    }
}
