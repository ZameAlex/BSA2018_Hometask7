using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BSA2018_Hometask4.Shared.DTO;

namespace BSA2018_Hometask4.BLL.Interfaces
{
    public interface IFlightService
    {
        Task<FlightDto> Get(int id);
        Task<List<FlightDto>> Get();
        Task<int> Create(FlightDto flight);
        Task Delete(int id);
        Task Delete(FlightDto flight);
        Task Update(FlightDto flight, int id);
        Task Update(DateTime departureTime, DateTime destinationTime, int id);
        Task<List<FlightDto>> GetWithDelay(double delay);


    }
}
