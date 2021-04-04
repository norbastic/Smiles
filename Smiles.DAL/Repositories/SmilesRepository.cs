using Smiles.Core.Models;
using Smiles.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smiles.DAL.Repositories
{
    public class SmilesRepository : Repository<SmilesEntity>, ISmilesRepository
    {
        public SmilesRepository(SmilesDbContext dbContext): base(dbContext)
        { }
    }
}
