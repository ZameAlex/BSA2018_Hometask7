using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BSA2018_Hometask4.DAL.DbContext;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class CrewRepository : BaseRepository<Crew>
    {
        public CrewRepository(AirportContext db):base(db)
        {
            
        }
        public override async Task Update(Crew entity, int id)
        {
            var temp =  await DbContext.SetOf<Crew>().FindAsync(id);
            temp.Pilot = entity.Pilot;
            temp.Stewadresses = entity.Stewadresses;
            DbContext.Crew.Update(temp);
            await base.Update(entity, id);
        }
    }
}
