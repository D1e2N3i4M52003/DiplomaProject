using Business.Authorization;
using Business.Helpers;
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
        private readonly IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public UserService(IUserRepository repository, IJwtUtils jwtUtils, IOptions<AppSettings> appSettings, IExcursionRepository excursionRepository)
        {
            _repository = repository;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
            _excursionRepository = excursionRepository;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            User? user = await _repository.GetByAsync(x => x.Username == model.Username);

            if (user is null)
            {
                throw new ArgumentException("No such user exists!");
            }

            // validate
            if (!BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful so generate jwt token
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }

        public async Task CreateAsync(CreateUserRequest model)
        {
            User user = new User
            {
                Id = Guid.NewGuid(),
                Username = model.Username,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Email = model.Email,
                CreationDate = DateTime.Now,
                Role = (Role)Enum.Parse(typeof(Role), "Moderator"),
                PasswordHash = BCryptNet.HashPassword(model.Password)
            };
            await _repository.CreateAsync(user);
        }

        public async Task EditAsync(EditUserRequest model)
        {

            User user = new User
            {
                Username = model.Username,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Email = model.Email
            };

            await _repository.UpdateAsync(user);
        }

        public async Task<UserModel> GetById(Guid id)
        {
            User? user = await _repository.GetByIdAsync(id);
            if (user is null)
            {
                throw new ArgumentException("No such user exists!");
            }
            UserModel userModel = new UserModel
            {
                Username = user.Username,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email
            };
            return userModel;
        }

        public async Task<UserModel> GetByAsync(Expression<Func<User, bool>> filter)
        {
            User? user = await _repository.GetByAsync(filter);
            if (user is null)
            {
                throw new ArgumentException("No such user exists!");
            }
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
            List<User> users = await _repository.GetAll().Select(u => u).ToListAsync();
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
            List<User> users = await _repository.GetAll(filter).Select(u => u).ToListAsync();
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

        public async Task ChangePassword(Guid id, ChangePasswordRequest model)
        {
            User? user = await _repository.GetByAsync(x => x.Id == id);

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

        public async Task ReserveExcursion(ExcursionModel model,Guid id)
        {
            Excursion? excursion = await _excursionRepository.GetByIdAsync(model.Id);
            if (excursion is null)
            {
                throw new ArgumentException("No such excursion exists!");
            }
            User? user = await _repository.GetByIdAsync(model.Id);
            if (user is null)
            {
                throw new ArgumentException("No such user exists!");
            }
            excursion.Participants.Add(user);
            user.Excursions.Add(excursion);
        }

    }
}
