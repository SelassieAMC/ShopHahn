using AutoMapper;
using FluentValidation;
using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using Hahn.ApplicationProcess.February2021.Domain.ResponseObjects;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.BL
{
    public class AssetService : IAssetService
    {
        private readonly IValidator<AssetDto> _assetValidator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uOw;

        public AssetService(IValidator<AssetDto> assetValidator, IMapper mapper, IUnitOfWork uOw)
        {
            _assetValidator = assetValidator;
            _mapper = mapper;
            _uOw = uOw;
        }

        public async Task<UnitResult<AssetDto>> CreateAsset(AssetDto assetDto)
        {
            var result = new UnitResult<AssetDto>();
            try
            {
                var eval = await IsValidAsset(assetDto);
                if (!eval.Item1)
                {
                    result.ValidationFailure(eval.Item2);
                    return result;
                }
                var asset = _mapper.Map<Asset>(assetDto);
                await _uOw.AssetRepository.CreateAsync(asset);
                _uOw.Commit();

                var data = _mapper.Map<AssetDto>(await _uOw.AssetRepository.GetByIdAsync(asset.ID));
                result.Success(data);
                return result;
            }
            catch (Exception ex)
            {
                result.Failure(ex.Message);
                return result;
            }
        }

        public async Task<UnitResult<AssetDto>> DeleteAsset(int Id)
        {
            var result = new UnitResult<AssetDto>();
            try
            {
                var entity = await _uOw.AssetRepository.GetByIdAsync(Id);
                if(entity is null)
                {
                    result.Failure("Not found!");
                    return result;
                }
                _uOw.AssetRepository.Delete(entity);
                _uOw.Commit();

                result.Success();
                return result;
            }
            catch (Exception ex)
            {
                result.Failure(ex.Message);
                return result;
            }
        }

        public async Task<UnitResult<AssetDto>> GetAssetById(int Id)
        {
            var result = new UnitResult<AssetDto>();
            try
            {
                var data = await _uOw.AssetRepository.GetByIdAsync(Id);
                if(data is null)
                {
                    result.Failure("Asset not found!");
                }
                result.Success(_mapper.Map<AssetDto>(data));
                return result;
            }
            catch (Exception ex)
            {
                result.Failure(ex.Message);
                return result;
            }
        }

        public async Task<UnitResult<AssetDto>> UpdateAsset(AssetDto assetDto)
        {
            var result = new UnitResult<AssetDto>();
            try
            {
                var eval = await IsValidAsset(assetDto);
                if (!eval.Item1)
                {
                    result.Failure(eval.Item2);
                    return result;
                }
                var asset = _mapper.Map<Asset>(assetDto);
                if (await _uOw.AssetRepository.GetByIdAsync(asset.ID) is null)
                {
                    result.Failure("Asset not found!");
                    return result;
                }
                _uOw.AssetRepository.UpdateAsync(asset);
                _uOw.Commit();
                var data = await _uOw.AssetRepository.GetByIdAsync(asset.ID);

                result.Success(_mapper.Map<AssetDto>(data));
                return result;
            }
            catch (Exception ex)
            {
                result.Failure(ex.Message);
                return result;
            }
        }

        private async Task<(bool, string)> IsValidAsset(AssetDto assetDto)
        {
            var result = await _assetValidator.ValidateAsync(assetDto);
            var sb = new StringBuilder();
            //return properties and errors violated
            if (!result.IsValid)
            {
                foreach(var error in result.Errors)
                {
                    sb.Append($"| Invalid value at property {error.PropertyName}, {error.ErrorMessage} |");
                }
            }
            return (result.IsValid, sb.ToString());
        }
    }
}
