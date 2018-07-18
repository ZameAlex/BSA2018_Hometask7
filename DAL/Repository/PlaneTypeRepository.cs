using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BSA2018_Hometask4.DAL.DbContext;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class PlaneTypeRepository : BaseRepository<PlaneType>
    {
        public PlaneTypeRepository(AirportContext db) : base(db)
        {
        }

        public override async Task Update(PlaneType entity, int id)
        {
            var temp = await DbContext.SetOf<PlaneType>().FindAsync(id);
            temp.MaxHeight = entity.MaxHeight;
            temp.MaxMass = entity.MaxMass;
            temp.Model = entity.Model;
            temp.Places = entity.Places;
            temp.Speed = entity.Speed;
            DbContext.Types.Update(temp);
            await base.Update(entity, id);
        }
    }

}

