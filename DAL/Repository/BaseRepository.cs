using DAL.Models;
using DAL.Repository;
using System.Collections.Generic;
using System.Linq;
using BSA2018_Hometask4.DAL.DbContext;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : DAL.Models.Entity
{
    //implement interface

    protected readonly AirportContext DbContext;

    public BaseRepository(AirportContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual async Task<int> Create(TEntity entity)
    {
        DbContext.SetOf<TEntity>().Add(entity);
        await DbContext.SaveChangesAsync();
        return entity.Id;
    }

    public virtual async Task Delete(TEntity entity)
    {
        DbContext.SetOf<TEntity>().Remove(entity);
        await DbContext.SaveChangesAsync();
    }

    public virtual async Task Delete(int id)
    {
        DbContext.SetOf<TEntity>().Remove( await DbContext.SetOf<TEntity>().FindAsync(id));
        await DbContext.SaveChangesAsync();
    }

    public virtual async Task<List<TEntity>> Get()
    {
        return  await DbContext.SetOf<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity> Get(int id)
    {
        return await DbContext.SetOf<TEntity>().FindAsync(id);
    }

    public virtual async Task Update(TEntity entity, int id)
    {
        await DbContext.SaveChangesAsync();
    }
}