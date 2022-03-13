using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Interfaces;

namespace DataLayer.Repositories
{
    public class ExcursionRepository : BaseRepository<Excursion>, IExcursionRepository
    {
        public ExcursionRepository(DBContext context) : base(context)
        {
        }
    }
}
