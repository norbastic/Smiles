using Smiles.Core;
using Smiles.Core.Models;
using Smiles.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smiles.BL
{
    public class SmilesService : ISmilesService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SmilesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SmilesEntity> CreateSmilesEntity(SmilesEntity smiles)
        {
            await _unitOfWork.SmilesEnities.AddAsync(smiles);
            await _unitOfWork.CommitAsync();
            
            return smiles;
        }

        public async Task DeleteSmilesEntity(int id)
        {
            _unitOfWork.SmilesEnities.Remove(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<SmilesEntity>> GetAllSmilesEntities()
        {
            return await _unitOfWork.SmilesEnities.GetAllAsync();
        }

        public async Task<SmilesEntity> GetSmilesEntityById(int id)
        {
            return await _unitOfWork.SmilesEnities.GetByIdAsync(id);
        }

        public async Task UpdateSmilesEntity(SmilesEntity smilesToUpdate, SmilesEntity smiles)
        {
            smilesToUpdate.Data = smiles.Data;

            await _unitOfWork.CommitAsync();
        }
    }
}
