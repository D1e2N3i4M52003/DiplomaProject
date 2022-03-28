using Business.JSONModels;
using DataLayer.Models;
using Business.Interfaces;
using DataLayer.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ExcursionService : IExcursionService
    {
        private readonly IExcursionRepository _repository;
        
        private readonly IDestinationRepository _destinationRepository;

        public ExcursionService(IExcursionRepository repository, IDestinationRepository destinationRepository)
        {
            _repository = repository;
            _destinationRepository = destinationRepository;
        }

        public async Task CreateAsync(ExcursionModel model)
        {
            Excursion excursion = new Excursion
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                CreationDate = DateTime.Now,
                EndsOnDate = model.EndsOnDate,
                StartsOnDate = model.StartsOnDate,
                Price = model.Price,
            };
            await _repository.CreateAsync(excursion);
        }

        public async Task EditAsync(ExcursionModel model)
        {

            Excursion excursion = new Excursion
            {
                Name = model.Name,
                EndsOnDate = model.EndsOnDate,
                StartsOnDate = model.StartsOnDate,
                Price = model.Price,
            };

            await _repository.UpdateAsync(excursion);
        }

        public async Task<ExcursionModel> GetById(Guid id)
        {
            Excursion? excursion = await _repository.GetByIdAsync(id);
            if (excursion is null)
            {
                throw new ArgumentException("No such excursion exists!");
            }
            ExcursionModel excursionModel = new ExcursionModel
            {
                Name = excursion.Name,
                EndsOnDate = excursion.EndsOnDate,
                StartsOnDate = excursion.StartsOnDate,
                Price = excursion.Price,
            };
            return excursionModel;
        }

        public async Task<List<DestinationModel>> GetAllDestinations(Guid id)
        {
            Excursion? excursion = await _repository.GetByIdAsync(id);
            if (excursion is null)
            {
                throw new ArgumentException("No such user exists!");
            }
            List<DestinationModel> destinations = new List<DestinationModel>();
            foreach (var destination in excursion.Destinations)
            {
                DestinationModel destinationModel = new DestinationModel
                {
                    City = destination.City,
                    Name = destination.Name,
                };
                destinations.Add(destinationModel);
            }
            return destinations;
        }

        public async Task<ExcursionModel> GetByAsync(Expression<Func<Excursion, bool>> filter)
        {
            Excursion? excursion = await _repository.GetByAsync(filter);
            if (excursion is null)
            {
                throw new ArgumentException("No such excursion exists!");
            }
            ExcursionModel excursionModel = new ExcursionModel
            {
                Name = excursion.Name,
                EndsOnDate = excursion.EndsOnDate,
                StartsOnDate = excursion.StartsOnDate,
                Price = excursion.Price,
            };
            return excursionModel;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<ExcursionModel>> GetAll()
        {
            List<Excursion> excursions = await _repository.GetAll().Select(d => d).ToListAsync();
            List<ExcursionModel> excursionModels = new List<ExcursionModel>();
            foreach (var excursion in excursions)
            {
                ExcursionModel excursionModel = new ExcursionModel
                {
                    Name = excursion.Name,
                    EndsOnDate = excursion.EndsOnDate,
                    StartsOnDate = excursion.StartsOnDate,
                    Price = excursion.Price,
                };
                excursionModels.Add(excursionModel);
            }
            return excursionModels;
        }
        public async Task<List<ExcursionModel>> GetAll(Expression<Func<Excursion, bool>> filter)
        {
            List<Excursion> excursions = await _repository.GetAll().Select(d => d).ToListAsync();
            List<ExcursionModel> excursionModels = new List<ExcursionModel>();
            foreach (var excursion in excursions)
            {
                ExcursionModel excursionModel = new ExcursionModel
                {
                    Name = excursion.Name,
                    EndsOnDate = excursion.EndsOnDate,
                    StartsOnDate = excursion.StartsOnDate,
                    Price = excursion.Price,
                };
                excursionModels.Add(excursionModel);
            }
            return excursionModels;
        }
        public async Task AddDestination(DestinationModel model)
        {
            Destinations? destination = await _destinationRepository.GetByIdAsync(model.Id);
            if (destination is null)
            {
                throw new ArgumentException("No such destination exists!");
            }
            Excursion? excursion = await _repository.GetByIdAsync(model.Id);
            if (excursion is null)
            {
                throw new ArgumentException("No such excursion exists!");
            }
            excursion.Destinations.Add(destination);
            destination.Excursions.Add(excursion);
        }
    }
}
