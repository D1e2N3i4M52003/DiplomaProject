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
                Destinations = new List<Destinations>(),
            };
            await _repository.CreateAsync(excursion);
        }

        public async Task EditAsync(ExcursionModel model)
        {

            Excursion excursion = new Excursion
            {
                Id = model.Id,
                Name = model.Name,
                EndsOnDate = model.EndsOnDate,
                StartsOnDate = model.StartsOnDate,
                Price = model.Price,
            };
            excursion.Destinations = new List<Destinations>();
            foreach (var destinationModel in model.Destinations)
            {
                
                Destinations? destination = await _destinationRepository.GetByIdAsync(destinationModel.Id);
                if (destination is null)
                {
                    throw new ArgumentException("No such destination exists!");
                }
                excursion.Destinations.Add(destination);
                if (destination.Excursions is null)
                {
                    destination.Excursions = new List<Excursion>();
                }
            }
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
                Id = excursion.Id,
                Name = excursion.Name,
                EndsOnDate = excursion.EndsOnDate,
                StartsOnDate = excursion.StartsOnDate,
                Price = excursion.Price,
                Destinations = new List<DestinationModel>()
            };
            List<DestinationModel> destinations = new List<DestinationModel>();
            foreach (var destination in excursion.Destinations)
            {
                Destinations dest = await _destinationRepository.GetByIdAsync(destination.Id);
                DestinationModel destinationModel = new DestinationModel
                {
                    Id = dest.Id,
                    City = dest.City,
                    Name = dest.Name,
                };
                excursionModel.Destinations.Add(destinationModel);
            }
            return excursionModel;
        }


        public async Task<ExcursionModel> GetByAsync(Expression<Func<Excursion, bool>> filter)
        {
            ICollection<Excursion?> excursions = await _repository.GetByAsync(filter);
            if (excursions.Count==0)
            {
                throw new ArgumentException("No such excursion exists!");
            }
            Excursion excursion = excursions.First();
            ExcursionModel excursionModel = new ExcursionModel
            {
                Id = excursion.Id,
                Name = excursion.Name,
                EndsOnDate = excursion.EndsOnDate,
                StartsOnDate = excursion.StartsOnDate,
                Price = excursion.Price,
            };
            List<DestinationModel> destinations = new List<DestinationModel>();
            foreach (var destination in excursion.Destinations)
            {
                Destinations dest = await _destinationRepository.GetByIdAsync(destination.Id);
                DestinationModel destinationModel = new DestinationModel
                {
                    Id = dest.Id,
                    City = dest.City,
                    Name = dest.Name,
                };
                excursionModel.Destinations.Add(destinationModel);
            }
            return excursionModel;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<ExcursionModel>> GetAll()
        {
            List<Excursion> excursions = _repository.GetAll().Result.ToList();
            List<ExcursionModel> excursionModels = new List<ExcursionModel>();
            foreach (var excursion in excursions)
            {
                
                ExcursionModel excursionModel = new ExcursionModel
                {
                    Id = excursion.Id,
                    Name = excursion.Name,
                    EndsOnDate = excursion.EndsOnDate,
                    StartsOnDate = excursion.StartsOnDate,
                    Price = excursion.Price,
                    Destinations = new List<DestinationModel>()
            };
                if (excursion.Destinations == null)
                { excursionModel.Destinations = new List<DestinationModel>(); }
                else
                {
                    foreach (var destination in excursion.Destinations)
                    {
                        Destinations dest = await _destinationRepository.GetByIdAsync(destination.Id);
                        DestinationModel destinationModel = new DestinationModel
                        {
                            Id = dest.Id,
                            City = dest.City,
                            Name = dest.Name,
                            Description = dest.Description
                        };
                        excursionModel.Destinations.Add(destinationModel);
                    }
                }
                excursionModels.Add(excursionModel);
            }
            return excursionModels;
        }
        public async Task<List<ExcursionModel>> GetAll(Expression<Func<Excursion, bool>> filter)
        {
            List<Excursion> excursions = _repository.GetAll().Result.ToList();
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
    }
}
