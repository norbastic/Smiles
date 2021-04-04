using Smiles.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smiles.Core.Services
{
    public interface ISmilesService
    {
        Task<IEnumerable<SmilesEntity>> GetAllSmilesEntities();
        Task<SmilesEntity> GetSmilesEntityById(int id);
        Task<SmilesEntity> CreateSmilesEntity(SmilesEntity smiles);
        Task UpdateSmilesEntity(SmilesEntity smilesToUpdate, SmilesEntity smiles);
        Task DeleteSmilesEntity(SmilesEntity smiles); 
    }
}
