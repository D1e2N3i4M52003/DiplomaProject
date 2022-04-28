using Business.JSONModels;
using DataLayer.Interfaces;
using DataLayer.Models;
using Business.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly IDestinationRepository _repository;

        public DestinationService(IDestinationRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(DestinationModel model)
        {
            Destinations destination = new Destinations
            {
                Id = Guid.NewGuid(),
                City = model.City,
                Name = model.Name,
                Description = model.Description,
            };
            await _repository.CreateAsync(destination);
        }

        public async Task EditAsync(DestinationModel model)
        {
            Destinations destination = await _repository.GetByIdAsync(model.Id);

            if (destination == null) throw new KeyNotFoundException("Destination not found");


            destination.City = model.City;
            destination.Description = model.Description;
            destination.Name = model.Name;
            

            await _repository.UpdateAsync(destination);
        }

        public async Task<DestinationModel> GetById(Guid id)
        {
            Destinations? destination = await _repository.GetByIdAsync(id);
            if (destination is null)
            {
                throw new ArgumentException("No such destination exists!");
            }
            DestinationModel destinationModel = new DestinationModel
            {
                Id = destination.Id,
                City = destination.City,
                Name = destination.Name,
                Description = destination.Description,
            };
            return destinationModel;
        }

        public async Task<DestinationModel> GetByAsync(Expression<Func<Destinations, bool>> filter)
        {
            ICollection<Destinations?> destinations = await _repository.GetByAsync(filter);
            if (destinations.Count==0)
            {
                throw new ArgumentException("No such user exists!");
            }
            Destinations destination = destinations.First();
            DestinationModel destinationModel = new DestinationModel
            {
                Id = destination.Id,
                City = destination.City,
                Name = destination.Name,
                Description = destination.Description,
            };
            return destinationModel;
        }

        public async Task DeleteAsync(Guid id)
        {

            await _repository.DeleteAsync(id);
        }

        public async Task<List<DestinationModel>> GetAll()
        {
            List<Destinations> destinations = _repository.GetAll().Result.ToList();
            List<DestinationModel> destinationsModel = new List<DestinationModel>();
            foreach (var destination in destinations)
            {
                DestinationModel destinationModel = new DestinationModel
                {
                    Id = destination.Id,
                    City = destination.City,
                    Name = destination.Name,
                    Description = destination.Description,
                };
                destinationsModel.Add(destinationModel);
            }
            return destinationsModel;
        }
        public async Task<List<DestinationModel>> GetAll(Expression<Func<Destinations, bool>> filter)
        {
            ICollection<Destinations> destinations = await _repository.GetAll(filter);
            List<DestinationModel> destinationsModel = new List<DestinationModel>();
            foreach (var destination in destinations)
            {
                DestinationModel destinationModel = new DestinationModel
                {
                    Id = destination.Id,
                    City = destination.City,
                    Name = destination.Name,
                    Description = destination.Description,
                };
                destinationsModel.Add(destinationModel);
            }
            return destinationsModel;
        }
    }
}
