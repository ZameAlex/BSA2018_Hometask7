﻿
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
    public class CrewService : ICrewService
    {
        readonly IUnitOfWork unit;
        readonly IMapper mapper;
        readonly AbstractValidator<CrewDto> validator;

        public CrewService(IUnitOfWork uow, IMapper map, AbstractValidator<CrewDto> rules)
        {
            unit = uow;
            mapper = map;
            validator = rules;
        }
        public async Task<int> Create(CrewDto Crew)
        {
            var validationResult = validator.Validate(Crew);
            if (validationResult.IsValid)
            {
                return await unit.Crew.Create(await mapper.MapCrew(Crew));
            }
            else
                throw new ValidationException(validationResult.Errors);
        }

        public async Task Delete(int id)
        {
            await unit.Crew.Delete(id);
        }

        public async Task Delete(CrewDto Crew)
        {
            await unit.Crew.Delete(await mapper.MapCrew(Crew));
        }

        public async Task<CrewDto> Get(int id)
        {
            return mapper.MapCrew(await unit.Crew.Get(id));
        }

        public async Task<List<CrewDto>> Get()
        {
            var result = new List<CrewDto>();
            var list = await unit.Crew.Get();
            foreach (var item in list)
            {
                result.Add(mapper.MapCrew(item));
            }
            return result;
        }

        public async Task Update(CrewDto Crew, int id)
        {
            var validationResult = validator.Validate(Crew);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            try
            {
                Crew.ID = id;
                await unit.Crew.Update(await mapper.MapCrew(Crew), id);
            }
            catch(ArgumentNullException)
            {
                throw new NotFoundException();
            }
            catch(Exception)
            {
                throw;
            }
        }

    }
}
