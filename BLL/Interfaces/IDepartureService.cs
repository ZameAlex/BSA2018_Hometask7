using BSA2018_Hometask4.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSA2018_Hometask4.BLL.Interfaces
{
    public interface IDepartureService
    {
        Task<DepartureDto> Get(int id);
        Task<List<DepartureDto>> Get();
        Task<int> Create(DepartureDto flight);
        Task Delete(int id);
        Task Delete(DepartureDto flight);
        Task Update(DepartureDto flight, int id);
        Task Update(DateTime date,int id);

    }
}
