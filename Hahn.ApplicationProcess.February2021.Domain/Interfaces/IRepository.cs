using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Interfaces
{
    public interface IRepository<T>
    {
        public Task CreateAsync(T entity);
        public Task<T> GetByIdAsync(int id);
        public Task UpdateAsync(int id, T entity);
        public void Delete(int id);
    }
}
