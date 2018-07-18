using BSA2018_Hometask4.BLL.Interfaces;
using BSA2018_Hometask4.Shared.DTO;
using DAL.Models;
using DAL.Repository;
using DAL.UnitOfWork;
using FluentValidation;
using System;
using System.Collections.Generic;
using BSA2018_Hometask4.Shared.Exceptions;
using System.Text;
using System.Threading.Tasks;

namespace BSA2018_Hometask4.BLL.Services
{
    public class DepartureService : IDepartureService
    {
        readonly IUnitOfWork unit;
        readonly IMapper mapper;
        readonly AbstractValidator<DepartureDto> validator;

        public DepartureService(IUnitOfWork uow, IMapper map, AbstractValidator<DepartureDto> rules)
        {
            unit = uow;
            mapper = map;
            validator = rules;
        }
        public async Task<int> Create(DepartureDto departure)
        {
            var validationResult = validator.Validate(departure);
            if (validationResult.IsValid)
                return await unit.Departures.Create(await mapper.MapDeparture(departure));
            else
                throw new ValidationException(validationResult.Errors);
            
        }

        public async Task Delete(int id)
        {
            await unit.Departures.Delete(id);
        }

        public async Task Delete(DepartureDto departure)
        {
            await unit.Departures.Delete(await mapper.MapDeparture(departure));
        }

        public async Task<DepartureDto> Get(int id)
        {
            return mapper.MapDeparture(await unit.Departures.Get(id));
        }

        public async Task<List<DepartureDto>> Get()
        {
            var result = new List<DepartureDto>();
            var list = await unit.Departures.Get();
            foreach (var item in list)
            {
                result.Add(mapper.MapDeparture(item));
            }
            return result;
        }

        public async Task Update(DepartureDto departure, int id)
        {
            var validationResult = validator.Validate(departure);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            try
            {
                departure.ID = id;
                await unit.Departures.Update(await mapper.MapDeparture(departure), id);
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

        public async Task Update(DateTime date, int id)
        {
            var validationResult = validator.Validate(new DepartureDto() { Date = date }, "Date");
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            try
            {
                await (unit.Departures as DepartureRepository).Update(date, id);
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
