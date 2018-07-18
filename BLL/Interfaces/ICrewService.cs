using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BSA2018_Hometask4.Shared.DTO;

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
        
    }
}
