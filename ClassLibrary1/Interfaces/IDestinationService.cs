using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using Business.JSONModels;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface IDestinationService
    {
        Task<DestinationModel> GetById(Guid id);
        Task<DestinationModel> GetByAsync(Expression<Func<Destinations, bool>> filter);
        Task CreateAsync(DestinationModel model);
        Task EditAsync(DestinationModel model);
        Task DeleteAsync(Guid id);
        Task<List<DestinationModel>> GetAll();
        Task<List<DestinationModel>> GetAll(Expression<Func<Destinations, bool>> filter);

    }
}
