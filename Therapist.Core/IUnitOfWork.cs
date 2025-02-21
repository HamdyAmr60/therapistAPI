using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Therapist.Core.Models;
using Therapist.Core.Reposatories;

namespace Therapist.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> SavaAsync();
        IGenericReposatory<T> repo<T>() where T : BaseEntity;
    }
}
