using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Therapist.Core;
using Therapist.Core.Models;
using Therapist.Core.Reposatories;
using Therapist.Reposatories.Data;
using Therapist.Reposatories.Reposatories;

namespace Therapist.Reposatories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private Hashtable _reposatories; 
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _reposatories = new Hashtable();
            this._dbContext = dbContext;
        }
        public async ValueTask DisposeAsync()
        {
          await _dbContext.DisposeAsync();
        }

        public IGenericReposatory<T> repo<T>() where T : BaseEntity
        {
            var Type= typeof(T).Name;
            if (!_reposatories.ContainsKey(Type))
            {
                var repo = new GenericReposatory<T>(_dbContext);
                _reposatories.Add(Type,repo);
            }

            return _reposatories[Type] as GenericReposatory<T>;
        }

        public Task<int> SavaAsync()
        {
           return _dbContext.SaveChangesAsync();
        }
    }
}
