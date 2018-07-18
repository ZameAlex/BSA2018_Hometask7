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
    public class TicketService : ITicketService
    {
        readonly IUnitOfWork unit;
        readonly IMapper mapper;
        readonly AbstractValidator<TicketDto> validator;

        public TicketService(IUnitOfWork uow, IMapper map, AbstractValidator<TicketDto> rules)
        {
            unit = uow;
            mapper = map;
            validator = rules;
        }
        public async Task<int> Create(TicketDto Ticket)
        {
            var validationResult = validator.Validate(Ticket);
            if (validationResult.IsValid)
                return await unit.Tickets.Create(await mapper.MapTicket(Ticket));
            else
                throw new ValidationException(validationResult.Errors);
            
        }

        public async Task Delete(int id)
        {
            await unit.Tickets.Delete(id);
        }

        public async Task Delete(TicketDto Ticket)
        {
            await unit.Tickets.Delete(await mapper.MapTicket(Ticket));
        }

        public async Task<TicketDto> Get(int id)
        {
            return mapper.MapTicket(await unit.Tickets.Get(id));
        }

        public async Task<List<TicketDto>> Get()
        {
            var result = new List<TicketDto>();
            foreach (var item in await unit.Tickets.Get())
            {
                result.Add(mapper.MapTicket(item));
            }
            return result;
        }

        public async Task Update(TicketDto Ticket, int id)
        {
            var validationResult = validator.Validate(Ticket);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            try
            {
                Ticket.ID = id;
                await unit.Tickets.Update(await mapper.MapTicket(Ticket), id);
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
