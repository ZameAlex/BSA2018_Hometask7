using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BSA2018_Hometask4.DAL.DbContext;
using System.Linq;

namespace DAL.Repository
{
    public class StewadressRepository : BaseRepository<Stewadress>
    {
        public StewadressRepository(AirportContext db):base(db)
        {

        }

        public override void Update(Stewadress entity, int id)
        {
            var temp = DbContext.SetOf<Stewadress>().SingleOrDefault(x => x.Id == id);
            temp.Birthday = entity.Birthday;
            temp.LastName = entity.LastName;
            temp.Name = entity.Name;
            DbContext.Stewadresses.Update(temp);
            base.Update(entity, id);
        }
    }
}
