using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningAPI.Domain.Repositories
{
    public interface IRepository<T> 
    {
        public Task<T> Get(Guid Id);
        public Task<IEnumerable<T>> GetAll();
        public Task Add(T entity);
        public Task Remove(T entity);
    }
}
