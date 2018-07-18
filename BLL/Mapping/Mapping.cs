using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using BSA2018_Hometask4.Shared.DTO;
using System.Linq;
using DAL.UnitOfWork;
using BSA2018_Hometask4.BLL.Interfaces;
using System.Threading.Tasks;

namespace BSA2018_Hometask4.BLL.Mapping
{
    public class Mapping:IMapper
    {
        readonly IUnitOfWork unitOfWork;
        public Mapping(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }
        public FlightDto MapFlight(Flight value)
        {
            return new FlightDto()
            {
                ID = value.Id,
                Number = value.Number,
                DeparturePoint = value.DeparturePoint,
                DepartureTime = value.DepartureTime,
                Destination = value.DestinationPoint,
                DestinationTime = value.DestinationTime,
                Tickets = value.Tickets.Select(u => u.Id).ToList()
            };
        }

        public async Task<Flight> MapFlight(FlightDto value)
        {
            var list = await unitOfWork.Tickets.Get();
            return new Flight()
            {
                Id=value.ID,
                DeparturePoint=value.DeparturePoint,
                DepartureTime=value.DepartureTime,
                DestinationPoint=value.Destination,
                DestinationTime=value.DestinationTime,
                Number=value.Number,
                Tickets= list.Where(t=>value.Tickets.Contains(t.Id)).ToList()
            };
        }

        public TicketDto MapTicket(Ticket value)
        {
            return new TicketDto
            {
                ID = value.Id,
                Number = value.Flight.Number,
                Price = value.Price
            };
        }

        public async Task<Ticket> MapTicket(TicketDto value)
        {
            var list = await unitOfWork.Flights.Get();
            return new Ticket
            {
                Id=value.ID,
                Flight=list.SingleOrDefault(x=>x.Number==value.Number),
                Price=value.Price
            };
        }

        public DepartureDto MapDeparture(Departure value)
        {
            return new DepartureDto
            {
                ID = value.Id,
                Date = value.Date,
                CrewId = value.Crew.Id,
                Number = value.Flight.Number,
                PlaneId = value.Plane.Id
            };
        }

        public async Task<Departure> MapDeparture(DepartureDto value)
        {
            var list = await unitOfWork.Flights.Get();
            return new Departure
            {
               Id=value.ID,
               Date=value.Date,
               Crew=await unitOfWork.Crew.Get(value.CrewId),
               Flight= list.SingleOrDefault(x=>x.Number==value.Number),
               Plane=await unitOfWork.Planes.Get(value.PlaneId)
            };
        }

        public StewadressDto MapStewadress(Stewadress value)
        {
            return new StewadressDto
            {
                ID = value.Id,
                FirstName = value.Name,
                LastName = value.LastName,
                Birthday = value.Birthday
            };
        }

        public Stewadress MapStewadress(StewadressDto value)
        {
            return new Stewadress
            {
                Id = value.ID,
                Name = value.FirstName,
                LastName = value.LastName,
                Birthday = value.Birthday
            };
        }

        public PilotDto MapPilot(Pilot value)
        {
            return new PilotDto
            {
                ID = value.Id,
                FirstName = value.Name,
                LastName = value.LastName,
                Birthday = value.Birthday,
                Experience = value.Experience
            };
        }

        public Pilot MapPilot(PilotDto value)
        {
            return new Pilot
            {
                Id = value.ID,
                Name = value.FirstName,
                LastName = value.LastName,
                Birthday = value.Birthday,
                Experience = value.Experience
            };
        }

        public CrewDto MapCrew(Crew value)
        {
            return new CrewDto
            {
                ID = value.Id,
                Pilot = value.Pilot.Id,
                Stewadress = value.Stewadresses.Select(x => x.Id).ToList()
            };
        }

        public async Task<Crew> MapCrew(CrewDto value)
        {
            var list = await unitOfWork.Stewadresses.Get();
            return new Crew
            {
                Id = value.ID,
                Pilot= await unitOfWork.Pilots.Get(value.Pilot),
                Stewadresses = list.Where(s=>value.Stewadress.Contains(s.Id)).ToList()
            };
        }

        public PlaneDto MapPlane(Plane value)
        {
            return new PlaneDto
            {
                ID = value.Id,
                Name = value.Name,
                Type = value.Type.Id,
                Created = value.Created,
                Expires = DateTime.Now - value.Expired
            };
        }

        public async Task<Plane> MapPlane(PlaneDto value)
        {
            return new Plane
            {
                Id = value.ID,
                Name = value.Name,
                Type = await unitOfWork.Types.Get(value.Type),
                Created = value.Created,
                Expired = DateTime.Now.AddDays(value.Expires.Days)
            };
        }

        public TypeDto MapType(PlaneType value)
        {
            return new TypeDto
            {
                ID = value.Id,
                FleightLength = value.FleightLength,
                MaxHeight = value.MaxHeight,
                MaxMass = value.MaxMass,
                Model = value.Model,
                Places = value.Places,
                Speed = value.Speed
            };
        }

        public PlaneType MapType(TypeDto value)
        {
            return new PlaneType
            {
                Id = value.ID,
                FleightLength = value.FleightLength,
                MaxHeight = value.MaxHeight,
                MaxMass = value.MaxMass,
                Model = value.Model,
                Places = value.Places,
                Speed = value.Speed
            };
        }

       
    }
}
