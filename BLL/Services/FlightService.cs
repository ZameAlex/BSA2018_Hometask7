using BSA2018_Hometask4.BLL.Interfaces;
using BSA2018_Hometask4.Shared.DTO;
using BSA2018_Hometask4.Shared.Exceptions;
using DAL.Models;
using DAL.Repository;
using DAL.UnitOfWork;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSA2018_Hometask4.BLL.Services
{
    public class FlightService : IFlightService
    {
        readonly IUnitOfWork unit;
        readonly IMapper mapper;
        readonly AbstractValidator<FlightDto> validator;

        public FlightService(IUnitOfWork uow, IMapper map, AbstractValidator<FlightDto> rules)
        {
            unit = uow;
            mapper = map;
            validator = rules;
        }
        public async Task<int> Create(FlightDto flight)
        {
            var validationResult = validator.Validate(flight);
            if (validationResult.IsValid)
                return await unit.Flights.Create(await mapper.MapFlight(flight));
            else
                throw new ValidationException(validationResult.Errors);
        }

        public async Task Delete(int id)
        {
            await unit.Flights.Delete(id);
        }

        public async Task Delete(FlightDto flight)
        {
            await unit.Flights.Delete(await mapper.MapFlight(flight));
        }

        public async Task<FlightDto> Get(int id)
        {
            return mapper.MapFlight(await unit.Flights.Get(id));
        }

        public async Task<List<FlightDto>> Get()
        {
            var result = new List<FlightDto>();
            var list = await unit.Flights.Get();
            foreach (var item in list)
            {
                result.Add(mapper.MapFlight(item));
            }
            return result;
        }

        public async Task Update(FlightDto flight, int id)
        {
            var validationResult = validator.Validate(flight);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            try
            {
                flight.ID = id;
                await unit.Flights.Update(await mapper.MapFlight(flight), id);
            }
            catch (ArgumentNullException)
            {
                throw new NotFoundException();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task Update(DateTime departureTime, DateTime destinationTime, int id)
        {
            var validationResult = validator.Validate(new FlightDto()
            {
                DepartureTime = departureTime,
                DestinationTime = destinationTime
            },
            "DepartureTime", "DestinationTime");

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            try
            {
                await  (unit.Flights as FlightRepository).Update(departureTime, destinationTime, id);
            }
            catch (ArgumentNullException)
            {
                throw new NotFoundException();
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
