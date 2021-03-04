using AutoparkWebEF.DAL.EF;
using Microsoft.EntityFrameworkCore;
using AutoparkWebEF.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkWebEF.DAL.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AutoparkContext db;
        private readonly DbSet<T> dbSet;

        public GenericRepository(AutoparkContext context)
        {
            db = context;
            dbSet = db.Set<T>();
        }

        public async Task Create(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await Get(id);
            dbSet.Remove(entity);
        }

        public virtual async Task<T> Get(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbSet.AsNoTracking();
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
