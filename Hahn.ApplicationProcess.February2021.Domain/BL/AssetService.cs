using AutoMapper;
using FluentValidation;
using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using Hahn.ApplicationProcess.February2021.Domain.ResponseObjects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        private readonly ILogger<AssetService> _logger;

        public AssetService(IValidator<AssetDto> assetValidator, IMapper mapper, IUnitOfWork uOw, ILogger<AssetService> logger)
        {
            _assetValidator = assetValidator;
            _mapper = mapper;
            _uOw = uOw;
            _logger = logger;
        }

        public async Task<UnitResult<AssetDto>> CreateAsset(AssetDto assetDto)
        {
            _logger.LogInformation($"Asset creation started {JsonConvert.SerializeObject(assetDto)}");
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
                _logger.LogInformation($"Asset creation ends, asset created: {JsonConvert.SerializeObject(data)}");
                result.Success(data);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while trying to create the asset object. {ex.Message}");
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
                    _logger.LogError($"Asset with id {Id} not found!");
                    result.Failure("Not found!");
                    return result;
                }
                _uOw.AssetRepository.Delete(entity);
                _uOw.Commit();
                _logger.LogInformation($"Asset with Id {Id} was deleted succesfully");
                result.Success();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while trying to delete the asset with Id {Id}. {ex.Message}");
                result.Failure(ex.Message, new AssetDto());
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
                    _logger.LogError($"Asset with id {Id} not found!");
                    result.Failure("Asset not found!");
                    return result;
                }
                result.Success(_mapper.Map<AssetDto>(data));
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while trying to get the asset with Id {Id}. {ex.Message}");
                result.Failure(ex.Message,new AssetDto());
                return result;
            }
        }

        public async Task<UnitResult<AssetDto>> UpdateAsset(AssetDto assetDto)
        {
            _logger.LogInformation($"Asset update started {JsonConvert.SerializeObject(assetDto)}");
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
                if (await _uOw.AssetRepository.GetByIdAsync(asset.ID) is null)
                {
                    _logger.LogError($"Asset with id {assetDto.ID} not found!");
                    result.Failure("Asset not found!");
                    return result;
                }
                _uOw.AssetRepository.UpdateAsync(asset);
                _uOw.Commit();
                var data = await _uOw.AssetRepository.GetByIdAsync(asset.ID);
                _logger.LogInformation($"Asset update ends, new values: {JsonConvert.SerializeObject(data)}");

                result.Success(_mapper.Map<AssetDto>(data));
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while trying to update the asset object. {ex.Message}");
                result.Failure(ex.Message, new AssetDto());
                return result;
            }
        }

        private async Task<(bool, string)> IsValidAsset(AssetDto assetDto)
        {
            var result = await _assetValidator.ValidateAsync(assetDto);
            var sb = new StringBuilder();

            if (!result.IsValid)
            {
                foreach(var error in result.Errors)
                {
                    sb.Append($"| Invalid value at property {error.PropertyName}, {error.ErrorMessage} |");
                }
                _logger.LogInformation($"The asset with the following properties has invalid values. {JsonConvert.SerializeObject(assetDto)} - Validation errors: /n {sb.ToString()}");
            }
            return (result.IsValid, sb.ToString());
        }
    }
}
