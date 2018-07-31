using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BSA2018_Hometask4.Shared.DTO;

namespace BSA2018_Hometask4.BLL.Interfaces
{
    public interface IStewadressService
    {
        Task<StewardessDto> Get(int id);
        Task<List<StewardessDto>> Get();
        Task<int> Create(StewardessDto flight);
        Task Delete(int id);
        Task Delete(StewardessDto flight);
        Task Update(StewardessDto flight, int id);
        
    }
}
