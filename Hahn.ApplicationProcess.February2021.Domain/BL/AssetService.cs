using FluentValidation;
using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using Hahn.ApplicationProcess.February2021.Domain.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.BL
{
    public class AssetService : IAssetService
    {
        private readonly IValidator<AssetDto> _assetValidator;

        public AssetService(IValidator<AssetDto> assetValidator)
        {
            _assetValidator = assetValidator;
        }

        public Task<UnitResult<AssetDto>> CreateAsset(AssetDto assetDto)
        {
            throw new NotImplementedException();
        }

        public UnitResult<AssetDto> DeleteAsset(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UnitResult<AssetDto>> GetAssetById(int Id)
        {
            await _assetValidator.ValidateAsync(new AssetDto());
            throw new NotImplementedException();
        }

        public Task<UnitResult<AssetDto>> UpdateAsset(int id, AssetDto assetDto)
        {
            throw new NotImplementedException();
        }
    }
}
