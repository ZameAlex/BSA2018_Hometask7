using BSA2018_Hometask4.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSA2018_Hometask4.BLL.Interfaces
{
    public interface IPilotService
    {
        Task<PilotDto>Get(int id);
        Task<List<PilotDto>> Get();
        Task<int> Create(PilotDto flight);
        Task Delete(int id);
        Task Delete(PilotDto flight);
        Task Update(PilotDto flight, int id);
        Task Update(int experience, int id);

    }
}
