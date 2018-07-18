using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BSA2018_Hometask4.Shared.DTO;

namespace BSA2018_Hometask4.BLL.Interfaces
{
    public interface IStewadressService
    {
        Task<StewadressDto> Get(int id);
        Task<List<StewadressDto>> Get();
        Task<int> Create(StewadressDto flight);
        Task Delete(int id);
        Task Delete(StewadressDto flight);
        Task Update(StewadressDto flight, int id);
        
    }
}
