using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BSA2018_Hometask4.DAL.DbContext;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class DepartureRepository : BaseRepository<Departure>
    {

        public DepartureRepository(AirportContext db):base(db)
        {
                
        }


        public override async Task Update(Departure entity, int id)
        {
            var temp = await DbContext.SetOf<Departure>().FindAsync(id);
            temp.Crew = entity.Crew;
            temp.Date = entity.Date;
            temp.Flight = entity.Flight;
            temp.Plane = entity.Plane;
            DbContext.Depatures.Update(temp);
            await base.Update(entity, id);
        }

        public async Task Update(DateTime date, int id)
        {
            var x = await Get(id);
            x.Date = date;
            await DbContext.SaveChangesAsync();
        }
    }
}
