using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.Persistence.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        internal ApplicationDbContext _context;
        internal DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async virtual Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Delete(int id)
        {
            T entityToDelete = _dbSet.Find(id);
            DeleteByEntity(entityToDelete);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual void UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void DeleteByEntity(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }
    }
}
