using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BSA2018_Hometask4.Shared.DTO;

namespace BSA2018_Hometask4.BLL.Interfaces
{
    public interface ITypeService
    {
        Task<TypeDto> Get(int id);
        Task<List<TypeDto>> Get();
        Task<int> Create(TypeDto flight);
        Task Delete(int id);
        Task Delete(TypeDto flight);
        Task Update(TypeDto flight, int id);

    }
}
