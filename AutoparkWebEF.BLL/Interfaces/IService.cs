using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AutoparkWebEF.BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        Task Create(T item);
        Task Delete(T item);
        Task Update(T item);
        Task<T> Get(int? id);
        IQueryable<T> GetAll();
        void Dispose();
    }
}
