using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly TradeMarketDbContext dbContext;
        private readonly DbSet<TEntity> dbSet;


        public GenericRepository(TradeMarketDbContext tradeMarket)
        {
            this.dbContext = tradeMarket;
            dbSet = dbContext.Set<TEntity>();
        }



        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }


        public void Delete(TEntity entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Deleted)
                dbSet.Attach(entity);

            dbSet.Remove(entity);
        }
        public async Task DeleteByIdAsync(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);

            if (entityToDelete == null) throw new InvalidOperationException("There is no records with such id");
            Delete(entityToDelete);

            await dbContext.SaveChangesAsync();
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.FirstAsync(el => el.Id == id);
        }


        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }
    }
}
