using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Business.JSONModels
{
    public class ChangePasswordRequest : BaseModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public string UserRole { get; set; }
        public Guid UserId { get; set; }
    }
}
