using AutoparkWebEF.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkWebEF.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> Get(int id);
        Task Create(T item);
        Task Delete(int id);
        void Update(T item);
    }
}
