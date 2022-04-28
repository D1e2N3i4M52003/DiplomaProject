using WebAPI.Authorization;
using WebAPI.Helpers;
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

namespace WebAPI.Authorization
{

    public interface IAuthenticate
    {
        public ValueTask<AuthenticateResponse> AuthenticateUser(AuthenticateRequest model);
    }

    public class Authenticate : IAuthenticate
    {
        private readonly IUserRepository _repository;
        private readonly IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public Authenticate(IUserRepository repository, IJwtUtils jwtUtils, IOptions<AppSettings> appSettings, IExcursionRepository excursionRepository)
        {
            _repository = repository;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
         }

        public async ValueTask<AuthenticateResponse> AuthenticateUser(AuthenticateRequest model)
        {
            ICollection<User>? users = await _repository.GetAll(x => x.Email == model.Email);

            if (users.Count==0)
            {
                throw new ArgumentException("No such user exists!");
            }
            User user = users.FirstOrDefault();

            // validate
            if (!BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful so generate jwt token
            var jwtToken = _jwtUtils.GenerateJwtToken(user);


            return new AuthenticateResponse(user, jwtToken);
        }
    }
}
