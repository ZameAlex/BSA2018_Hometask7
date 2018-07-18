using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BSA2018_Hometask4.DAL.DbContext;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class PlaneRepository : BaseRepository<Plane>
    {
        public PlaneRepository(AirportContext db) : base(db)
        {

        }

        public override async Task Update(Plane entity, int id)
        {
            var temp = await DbContext.SetOf<Plane>().FindAsync(id);
            temp.Created = entity.Created;
            temp.Expired = entity.Expired;
            temp.Type = entity.Type;
            temp.Name = entity.Name;
            DbContext.Planes.Update(temp);
            await base.Update(entity, id);
        }

        public async Task Update(DateTime expires, int id)
        {
            var temp = await Get(id);
            temp.Expired = expires;
            await DbContext.SaveChangesAsync();
        }
    }
}
