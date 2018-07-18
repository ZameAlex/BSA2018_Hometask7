using BSA2018_Hometask4.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSA2018_Hometask4.BLL.Interfaces
{
    public interface ITicketService
    {
        Task<TicketDto> Get(int id);
        Task<List<TicketDto>> Get();
        Task<int> Create(TicketDto flight);
        Task Delete(int id);
        Task Delete(TicketDto flight);
        Task Update(TicketDto flight, int id);

    }
}
