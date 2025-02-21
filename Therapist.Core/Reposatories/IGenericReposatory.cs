using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Therapist.Core.Models;

namespace Therapist.Core.Reposatories
{
    public interface IGenericReposatory<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        void Update(T entity);
        void Delete(int id);
        Task AddAsync(T entity);

    }
}
