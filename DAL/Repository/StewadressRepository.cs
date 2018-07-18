using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BSA2018_Hometask4.DAL.DbContext;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class StewadressRepository : BaseRepository<Stewadress>
    {
        public StewadressRepository(AirportContext db):base(db)
        {

        }

        public override async Task Update(Stewadress entity, int id)
        {
            var temp = await DbContext.SetOf<Stewadress>().FindAsync(id);
            temp.Birthday = entity.Birthday;
            temp.LastName = entity.LastName;
            temp.Name = entity.Name;
            DbContext.Stewadresses.Update(temp);
            await base.Update(entity, id);
        }
    }
}
