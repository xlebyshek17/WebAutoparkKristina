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

        public GenericRepository(AutoparkContext context)
        {
            db = context;
        }

        public async void Create(TEntity entity)
        {
            await db.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(int id)
        {
            var entity = Get(id).Result;
            db.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<TEntity> Get(int id)
        {
            return await db.Set<TEntity>().FindAsync(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return db.Set<TEntity>().AsNoTracking();
        }

        public void Update(TEntity entity)
        {
            db.Set<TEntity>().Update(entity);
        }
    }
}
