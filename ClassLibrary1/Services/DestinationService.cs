using Business.JSONModels;
using DataLayer.Repositories;
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
        private readonly DestinationRepository _repository;

        public DestinationService(DestinationRepository repository)
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
            };
            await _repository.CreateAsync(destination);
        }

        public async Task EditAsync(DestinationModel model)
        {
            Destinations destination = await _repository.GetByIdAsync(model.Id);

            if (destination == null) throw new KeyNotFoundException("Destination not found");


            destination.City = model.City;
            destination.Name = model.Name;
            

            await _repository.UpdateAsync(destination);
        }

        public async Task<DestinationModel> GetById(Guid id)
        {
            Destinations? destination = await _repository.GetByIdAsync(id);
            if (destination is null)
            {
                throw new ArgumentException("No such user exists!");
            }
            DestinationModel destinationModel = new DestinationModel
            {
                City = destination.City,
                Name = destination.Name,
            };
            return destinationModel;
        }

        public async Task<DestinationModel> GetByAsync(Expression<Func<Destinations, bool>> filter)
        {
            Destinations? destination = await _repository.GetByAsync(filter);
            if (destination is null)
            {
                throw new ArgumentException("No such user exists!");
            }
            DestinationModel destinationModel = new DestinationModel
            {
                City = destination.City,
                Name = destination.Name,
            };
            return destinationModel;
        }

        public async Task DeleteAsync(Guid id)
        {

            await _repository.DeleteAsync(id);
        }

        public async Task<List<DestinationModel>> GetAll()
        {
            List<Destinations> destinations = await _repository.GetAll().Select(d => d).ToListAsync();
            List<DestinationModel> destinationsModel = new List<DestinationModel>();
            foreach (var destination in destinations)
            {
                DestinationModel destinationModel = new DestinationModel
                {
                    City = destination.City,
                    Name = destination.Name,
                };
                destinationsModel.Add(destinationModel);
            }
            return destinationsModel;
        }
        public async Task<List<DestinationModel>> GetAll(Expression<Func<Destinations, bool>> filter)
        {
            List<Destinations> destinations = await _repository.GetAll(filter).Select(d => d).ToListAsync();
            List<DestinationModel> destinationsModel = new List<DestinationModel>();
            foreach (var destination in destinations)
            {
                DestinationModel destinationModel = new DestinationModel
                {
                    City = destination.City,
                    Name = destination.Name,
                };
                destinationsModel.Add(destinationModel);
            }
            return destinationsModel;
        }
    }
}
