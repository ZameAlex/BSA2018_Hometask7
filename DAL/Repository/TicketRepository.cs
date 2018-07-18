using BSA2018_Hometask4.DAL.DbContext;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class TicketRepository : BaseRepository<Ticket>
    {
        public TicketRepository(AirportContext db) : base(db)
        {

        }

        public override async Task Update(Ticket entity, int id)
        {
            var temp = await DbContext.SetOf<Ticket>().FindAsync(id);
            temp.Flight = entity.Flight;
            temp.Price = entity.Price;
            DbContext.Tickets.Update(temp);
            await base.Update(entity, id);
        }
    }
}
