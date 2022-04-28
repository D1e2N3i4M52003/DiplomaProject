using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.JSONModels
{
    public  class ReserveModel : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid ExcursionId { get; set; }
    }
}
