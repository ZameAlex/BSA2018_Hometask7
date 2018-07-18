
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
    public class PlaneService : IPlaneService
    {
        readonly IUnitOfWork unit;
        readonly IMapper mapper;
        readonly AbstractValidator<PlaneDto> validator;

        public PlaneService(IUnitOfWork uow, IMapper map, AbstractValidator<PlaneDto> rules)
        {
            unit = uow;
            mapper = map;
            validator = rules;
        }
        public async Task<int> Create(PlaneDto Plane)
        {
            var validationResult = validator.Validate(Plane);
            if (validationResult.IsValid)
                return await unit.Planes.Create(await mapper.MapPlane(Plane));
            else
                throw new ValidationException(validationResult.Errors);
            
        }

        public async Task Delete(int id)
        {
            await unit.Planes.Delete(id);
        }

        public async Task Delete(PlaneDto Plane)
        {
            await unit.Planes.Delete(await mapper.MapPlane(Plane));
        }

        public async Task<PlaneDto> Get(int id)
        {
            return mapper.MapPlane(await unit.Planes.Get(id));
        }

        public async Task<List<PlaneDto>> Get()
        {
            var result = new List<PlaneDto>();
            foreach (var item in await unit.Planes.Get())
            {
                result.Add(mapper.MapPlane(item));
            }
            return result;
        }

        public async Task Update(PlaneDto Plane, int id)
        {
            var validationResult = validator.Validate(Plane);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            try
            {
                Plane.ID = id;
                await unit.Planes.Update(await mapper.MapPlane(Plane), id);
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

        public async Task Update(TimeSpan expires, int id)
        {
            var validationResult = validator.Validate(new PlaneDto() { Expires = expires }, "Expires");
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            try
            {
                await (unit.Planes as PlaneRepository).Update(DateTime.Now.AddDays(expires.Days), id);
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
