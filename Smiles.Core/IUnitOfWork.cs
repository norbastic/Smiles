using Smiles.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smiles.Core
{
    public interface IUnitOfWork
    {
        ISmilesRepository SmilesEnities { get; }
        Task<int> CommitAsync();
    }
}
