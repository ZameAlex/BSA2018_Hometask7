using BSA2018_Hometask4.Shared.DTO;
using BSA2018_Hometask7.Shared.DTO.API;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSA2018_Hometask4.BLL.Interfaces
{
    public interface IMapper
    {
        CrewDto MapCrew(Crew value);
        Task<Crew> MapCrew(CrewDto value);

        PilotDto MapPilot(Pilot value);
        Pilot MapPilot(PilotDto value);

        StewadressDto MapStewadress(Stewadress value);
        Stewadress MapStewadress(StewadressDto value);

        PlaneDto MapPlane(Plane value);
        Task<Plane> MapPlane(PlaneDto value);

        TypeDto MapType(PlaneType value);
        PlaneType MapType(TypeDto value);

        FlightDto MapFlight(Flight value);
        Task<Flight> MapFlight(FlightDto value);

        DepartureDto MapDeparture(Departure value);
        Task<Departure> MapDeparture(DepartureDto value);

        TicketDto MapTicket(Ticket value);
        Task<Ticket> MapTicket(TicketDto value);


        List<Crew> MapCrewApi(List<APICrewDto> ApiCrewDtos);
        Pilot MapPilotApi(APIPilotDto pilot);
        Stewadress MapStewadressApi(APIStewardessDto stewardess);
        List<Stewadress> MapStewadressesApi(List<APIStewardessDto> stewardesses);
       
    }
}
