using AutoparkWebEF.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkWebEF.DAL.Interfaces
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly AutoparkContext db;
        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(AutoparkContext context)
        {
            db = context;
            dbSet = db.Set<TEntity>();
        }

        public async void Create(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Delete(int id)
        {
            var entity = Get(id).Result;
            dbSet.Remove(entity);
        }

        public virtual async Task<TEntity> Get(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsNoTracking();
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }
    }
}
