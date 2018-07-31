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
    public class StewardessService : IStewadressService
    {
        readonly IUnitOfWork unit;
        readonly IMapper mapper;
        readonly AbstractValidator<StewardessDto> validator;

        public StewardessService(IUnitOfWork uow, IMapper map, AbstractValidator<StewardessDto> rules)
        {
            unit = uow;
            mapper = map;
            validator = rules;
        }
        public async Task<int> Create(StewardessDto Stewadress)
        {
            var validationResult = validator.Validate(Stewadress);
            if (validationResult.IsValid)
                return await unit.Stewadresses.Create(mapper.MapStewadress(Stewadress));
            else
                throw new ValidationException(validationResult.Errors);

        }

        public async Task Delete(int id)
        {
            await unit.Stewadresses.Delete(id);
        }

        public async Task Delete(StewardessDto Stewadress)
        {
            await unit.Stewadresses.Delete(mapper.MapStewadress(Stewadress));
        }

        public async Task<StewardessDto> Get(int id)
        {
            return mapper.MapStewadress(await unit.Stewadresses.Get(id));
        }

        public async Task<List<StewardessDto>> Get()
        {
            var result = new List<StewardessDto>();
            foreach (var item in await unit.Stewadresses.Get())
            {
                result.Add(mapper.MapStewadress(item));
            }
            return result;
        }

        public async Task Update(StewardessDto Stewadress, int id)
        {
            var validationResult = validator.Validate(Stewadress);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            try
            {
                Stewadress.ID = id;
                await unit.Stewadresses.Update(mapper.MapStewadress(Stewadress), id);
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
