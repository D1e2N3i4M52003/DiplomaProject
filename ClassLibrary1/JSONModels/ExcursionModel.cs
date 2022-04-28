using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace Business.JSONModels
{
    public class ExcursionModel : BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartsOnDate { get; set; }
        public DateTime EndsOnDate { get; set; }
        public decimal Price { get; set; }
        public string UserRole { get; set; }
        public virtual List<DestinationModel> Destinations { get; set; }
    }
}
