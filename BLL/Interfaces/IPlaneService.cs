using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BSA2018_Hometask4.Shared.DTO;

namespace BSA2018_Hometask4.BLL.Interfaces
{
    public interface IPlaneService
    {
        Task<PlaneDto> Get(int id);
        Task<List<PlaneDto>> Get();
        Task<int> Create(PlaneDto flight);
        Task Delete(int id);
        Task Delete(PlaneDto flight);
        Task Update(PlaneDto flight, int id);
        Task Update(TimeSpan expires, int id);

    }
}
