using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Therapist.Core.Models;
using Therapist.Core.Reposatories;
using Therapist.Reposatories.Data;

namespace Therapist.Reposatories.Reposatories
{
    public class GenericReposatory<T> : IGenericReposatory<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericReposatory(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public void Delete(int id)
        {
           _dbContext.Remove(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
           return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
          return  await _dbContext.Set<T>().FindAsync(id);
        }

        public  void Update(T entity)
        {
             _dbContext.Set<T>().Update(entity);
        }
    }
}
