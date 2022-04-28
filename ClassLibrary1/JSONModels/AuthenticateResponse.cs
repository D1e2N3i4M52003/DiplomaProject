﻿using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.JSONModels
{
    public class AuthenticateResponse : BaseModel
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse()
        {
            
        }
        public AuthenticateResponse(User user, string token)
        {
            ID = user.Id;
            FirstName = user.Firstname;
            LastName = user.Lastname;
            Username = user.Username;
            Role = user.Role;
            Token = token;
        }
    }
}
