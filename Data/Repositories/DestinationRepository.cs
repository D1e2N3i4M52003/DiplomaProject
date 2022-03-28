using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Interfaces;

namespace DataLayer.Repositories
{
    public class DestinationRepository : BaseRepository<Destinations>, IDestinationRepository
    {
        public DestinationRepository(DBContext context) : base(context)
        {
        }
    }
}
