using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Business.JSONModels
{
    public class EditUserRequest : BaseModel
    {
         public Guid ID { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string UserRole { get; set; }
        public List<ExcursionModel> Excursions { get; set; }
    }
}
