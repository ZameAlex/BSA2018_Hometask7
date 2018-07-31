using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using BSA2018_Hometask4.Shared.DTO;
using System.Linq;
using DAL.UnitOfWork;
using BSA2018_Hometask4.BLL.Interfaces;
using System.Threading.Tasks;
using BSA2018_Hometask7.Shared.DTO.API;

namespace BSA2018_Hometask4.BLL.Mapping
{
    public class Mapping : IMapper
    {
        readonly IUnitOfWork unitOfWork;
        public Mapping(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }
        public FlightDto MapFlight(Flight value)
        {
            var list = new List<TicketDto>();
            foreach (var item in value.Tickets)
                list.Add(MapTicket(item));
            return new FlightDto()
            {
                ID = value.Id,
                Number = value.Number,
                DeparturePoint = value.DeparturePoint,
                DepartureTime = value.DepartureTime,
                Destination = value.DestinationPoint,
                DestinationTime = value.DestinationTime,
                Tickets = list
            };
        }

        public async Task<Flight> MapFlight(FlightDto value)
        {
            var list = await unitOfWork.Tickets.Get();
            var res = new List<Ticket>();
            foreach (var item in value.Tickets)
            {
                foreach (var tick in list)
                    if (item.ID == tick.Id)
                        res.Add(tick);
            }
            return new Flight()
            {
                Id = value.ID,
                DeparturePoint = value.DeparturePoint,
                DepartureTime = value.DepartureTime,
                DestinationPoint = value.Destination,
                DestinationTime = value.DestinationTime,
                Number = value.Number,
                Tickets = res

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
                Id = value.ID,
                Flight = list.SingleOrDefault(x => x.Number == value.Number),
                Price = value.Price
            };
        }

        public DepartureDto MapDeparture(Departure value)
        {
            return new DepartureDto
            {
                ID = value.Id,
                Date = value.Date,
                Crew = MapCrew(value.Crew),
                Number = value.Flight.Number,
                Plane = MapPlane(value.Plane)
            };
        }

        public async Task<Departure> MapDeparture(DepartureDto value)
        {
            var list = await unitOfWork.Flights.Get();
            return new Departure
            {
                Id = value.ID,
                Date = value.Date,
                Crew = await unitOfWork.Crew.Get(value.Crew.ID),
                Flight = list.SingleOrDefault(x => x.Number == value.Number),
                Plane = await unitOfWork.Planes.Get(value.Plane.ID)
            };
        }

        public StewardessDto MapStewadress(Stewardess value)
        {
            return new StewardessDto
            {
                ID = value.Id,
                FirstName = value.Name,
                LastName = value.LastName,
                Birthday = value.Birthday
            };
        }

        public Stewardess MapStewadress(StewardessDto value)
        {
            return new Stewardess
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
                Pilot = MapPilot(value.Pilot),
                Stewardess = MapStewadresses(value.Stewadresses)
            };
        }

        private List<StewardessDto> MapStewadresses(List<Stewardess> stewadresses)
        {
            var result = new List<StewardessDto>();
            foreach (var s in stewadresses)
                result.Add(MapStewadress(s));
            return result;
        }

        private List<Stewardess> MapStewadresses(List<StewardessDto> stewadresses)
        {
            var result = new List<Stewardess>();
            foreach (var s in stewadresses)
                result.Add(MapStewadress(s));
            return result;
        }

        public async Task<Crew> MapCrew(CrewDto value)
        {
            var list = await unitOfWork.Stewadresses.Get();
            var res = new List<Stewardess>();
            foreach (var item in value.Stewardess)
            {
                foreach (var stew in list)
                    if (item.ID == stew.Id)
                        res.Add(stew);
            }
            return new Crew
            {
                Id = value.ID,
                Pilot = await unitOfWork.Pilots.Get(value.Pilot.ID),
                Stewadresses = res
            };
        }

        public PlaneDto MapPlane(Plane value)
        {
            return new PlaneDto
            {
                ID = value.Id,
                Name = value.Name,
                Type = MapType(value.Type),
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
                Type = MapType(value.Type),
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
        #region APIMapping
        public List<Crew> MapCrewApi(List<APICrewDto> ApiCrewDtos)
        {
            var result = new List<Crew>();
            foreach (var crew in ApiCrewDtos)
            {
                result.Add(new Crew()
                {
                    Pilot = MapPilotApi(crew.Pilot.First()),
                    Stewadresses = MapStewadressesApi(crew.Stewardess)
                }
                );
            }
            return result;
        }

        public Pilot MapPilotApi(APIPilotDto pilot)
        {
            return new Pilot
            {
                Birthday = pilot.BirthDate,
                Experience = pilot.Exp,
                Name = pilot.FirstName,
                LastName = pilot.LastName
            };
        }

        public Stewardess MapStewadressApi(APIStewardessDto stewardess)
        {
            return new Stewardess
            {
                Birthday = stewardess.BirthDate,
                Name = stewardess.FirstName,
                LastName = stewardess.LastName
            };
        }

        public List<Stewardess> MapStewadressesApi(List<APIStewardessDto> stewardesses)
        {
            var result = new List<Stewardess>();
            foreach (var stewadress in stewardesses)
            {
                result.Add(MapStewadressApi(stewadress));
            }
            return result;
        }

        #endregion

    }
}
