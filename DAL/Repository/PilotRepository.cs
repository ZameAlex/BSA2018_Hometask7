using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BSA2018_Hometask4.DAL.DbContext;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class PilotRepository : BaseRepository<Pilot>
    {
        public PilotRepository(AirportContext db):base(db)
        {

        }


        public override async Task Update(Pilot entity, int id)
        {
            var temp = await DbContext.SetOf<Pilot>().FindAsync(id);
            temp.Birthday = entity.Birthday;
            temp.Experience = entity.Experience;
            temp.LastName = entity.LastName;
            temp.Name = entity.Name;
            DbContext.Pilots.Update(temp);
            await base.Update(entity, id);
        }

        public async Task Update(int experience,int id)
        {
            var temp = await Get(id);
            temp.Experience = experience;
            await DbContext.SaveChangesAsync();
        }
    }
}
