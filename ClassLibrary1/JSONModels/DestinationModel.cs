using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.JSONModels
{
    public class DestinationModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string City { get; set; }
    }
}
