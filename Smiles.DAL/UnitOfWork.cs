using Smiles.Core;
using Smiles.Core.Repositories;
using Smiles.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smiles.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SmilesDbContext _dbContext;
        private SmilesRepository _smilesRepository;

        public UnitOfWork(SmilesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ISmilesRepository SmilesEnities => _smilesRepository = _smilesRepository ?? new SmilesRepository(_dbContext);

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
