using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BSA2018_Hometask4.DAL.DbContext;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class StewadressRepository : BaseRepository<Stewardess>
    {
        public StewadressRepository(AirportContext db):base(db)
        {

        }

        public override async Task Update(Stewardess entity, int id)
        {
            var temp = await DbContext.SetOf<Stewardess>().FindAsync(id);
            temp.Birthday = entity.Birthday;
            temp.LastName = entity.LastName;
            temp.Name = entity.Name;
            DbContext.Stewardesses.Update(temp);
            await base.Update(entity, id);
        }
    }
}
