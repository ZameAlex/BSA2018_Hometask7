﻿using BSA2018_Hometask4.BLL.Interfaces;
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
    public class TypeService : ITypeService
    {
        readonly IUnitOfWork unit;
        readonly IMapper mapper;
        readonly AbstractValidator<TypeDto> validator;

        public TypeService(IUnitOfWork uow, IMapper map, AbstractValidator<TypeDto> rules)
        {
            unit = uow;
            mapper = map;
            validator = rules;
        }
        public async Task<int> Create(TypeDto Type)
        {
            var validationResult = validator.Validate(Type);
            if (validationResult.IsValid)
                return await unit.Types.Create(mapper.MapType(Type));
            else
                throw new ValidationException(validationResult.Errors);
        }

        public async Task Delete(int id)
        {
            await unit.Types.Delete(id);
        }

        public async Task Delete(TypeDto Type)
        {
            await unit.Types.Delete(mapper.MapType(Type));
        }

        public async Task<TypeDto> Get(int id)
        {
            return mapper.MapType(await unit.Types.Get(id));
        }

        public async Task<List<TypeDto>> Get()
        {
            var result = new List<TypeDto>();
            foreach (var item in await unit.Types.Get())
            {
                result.Add(mapper.MapType(item));
            }
            return result;
        }

        public async Task Update(TypeDto Type, int id)
        {
            var validationResult = validator.Validate(Type);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            try
            {
                Type.ID = id;
                await unit.Types.Update(mapper.MapType(Type), id);
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
