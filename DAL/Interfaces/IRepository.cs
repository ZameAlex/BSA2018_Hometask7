using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IRepository<TEntity> where TEntity:Entity
    {
        Task<List<TEntity>> Get();

        Task<TEntity> Get(int id);

        Task<int> Create(TEntity entity);

        Task Update(TEntity entity, int id);

        Task Delete(TEntity entity);

        Task Delete(int id);
    }
}
