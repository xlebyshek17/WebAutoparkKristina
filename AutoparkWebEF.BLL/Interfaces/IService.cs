using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkWebEF.BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        void Create(T item);
        void Delete(T item);
        void Update(T item);
        Task<T> Get(int? id);
        Task<IEnumerable<T>> GetAll();
        void Dispose();
    }
}
