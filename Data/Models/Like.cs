using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Like
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}
