using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.JSONModels
{
    public class DestinationModel : BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string City { get; set; }
        public string UserRole { get; set; }

        public string Description { get; set; }

    }
}
