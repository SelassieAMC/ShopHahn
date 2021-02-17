using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task CreateAsync(T entity);
        public Task<T> GetByIdAsync(int id);
        public void UpdateAsync(T entity);
        public void Delete(int id);
    }
}
