using BSA2018_Hometask4.DAL.DbContext;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class FlightRepository : BaseRepository<Flight>
    {

        public FlightRepository(AirportContext db) : base(db)
        {

        }


        public override async Task Update(Flight entity, int id)
        {
            var temp = await DbContext.SetOf<Flight>().FindAsync(id);
            temp.DeparturePoint = entity.DeparturePoint;
            temp.DepartureTime = entity.DepartureTime;
            temp.DestinationPoint = entity.DestinationPoint;
            temp.DestinationTime = entity.DestinationTime;
            temp.Number = entity.Number;
            temp.Tickets = entity.Tickets;
            DbContext.Flights.Update(temp);
            await base.Update(entity, id);
        }

        public async Task Update(DateTime departure, DateTime destination, int id)
        {
            var temp = await Get(id);
            temp.DepartureTime = departure;
            temp.DestinationTime = destination;
            DbContext.Update(temp);
            await DbContext.SaveChangesAsync();
        }
    }
}
