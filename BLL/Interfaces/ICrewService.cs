using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BSA2018_Hometask4.Shared.DTO;
using BSA2018_Hometask7.Shared.DTO.API;

namespace BSA2018_Hometask4.BLL.Interfaces
{
    public interface ICrewService
    {
        Task<CrewDto> Get(int id);
        Task<List<CrewDto>> Get();
        Task<int> Create(CrewDto flight);
        Task Delete(int id);
        Task Delete(CrewDto flight);
        Task Update(CrewDto flight, int id);

        Task WriteToDB(List<APICrewDto> crews);
        Task WriteToCsv(List<APICrewDto> crews);

    }
}
