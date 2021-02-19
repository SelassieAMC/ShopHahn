using AutoMapper;
using FluentValidation;
using Hahn.ApplicationProcess.February2021.Domain.BL;
using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using Hahn.ApplicationProcess.February2021.Domain.Models.Examples;
using Hahn.ApplicationProcess.February2021.Domain.ResponseObjects;
using Hahn.ApplicationProcess.February2021.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Hahn.ApplicationProcess.February2021.Test.Hahn.ApplicationProcess.February2021.Web.Test
{
    public class AssetControllerTest : SetupWebTest
    {
        private readonly Assetcontroller _assetController;
        private readonly IValidator<AssetDto> _validatorService;
        private readonly IMapper _mapperService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AssetService> _logger;

        public AssetControllerTest()
        {
            var services = GetServices();
            var serviceProvider = services.BuildServiceProvider();

            _validatorService = serviceProvider.GetService<IValidator<AssetDto>>();
            _mapperService = serviceProvider.GetService<IMapper>();
            _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
            _logger = serviceProvider.GetService<ILogger<AssetService>>();

            var assetService = new AssetService(_validatorService, _mapperService, _unitOfWork, _logger);
            _assetController = new Assetcontroller(assetService);
        }
        [Fact]
        public async Task CreateAssetShouldReturn201Status()
        {
            var asset = new AssetCreationRequestExample().GetExamples();

            var result = (ObjectResult) await _assetController.CreateAssetAsync(asset);
            var value = (UnitResult<AssetDto>) result.Value;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.True(value.EndOnSuccess);
            Assert.False(value.EndOnError);
            Assert.False(value.EndOnValidationError);
            Assert.NotEqual(asset.ID, value.Result.ID);
        }

        [Fact]
        public async Task CreateAssetShouldReturnBadRequestError()
        {
            var asset = new AssetCreationUpdateBadRequestExample().GetExamples();

            var result = (ObjectResult)await _assetController.CreateAssetAsync(asset);
            var value = (UnitResult<AssetDto>)result.Value;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.False(value.EndOnError);
            Assert.False(value.EndOnSuccess);
            Assert.True(value.EndOnValidationError);
            Assert.Null(value.Result);
        }

        [Fact]
        public async Task GetAssetByIdShouldReturnOK()
        {
            var id = 1;
            var result = (ObjectResult)await _assetController.GetAssetByIdAsync(id);
            var value = (UnitResult<AssetDto>)result.Value;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.False(value.EndOnError);
            Assert.True(value.EndOnSuccess);
            Assert.False(value.EndOnValidationError);
            Assert.NotNull(value.Result);
            Assert.Equal(id, value.Result.ID);
        }

        [Fact]
        public async Task GetAssetByIdShouldReturnNotFound()
        {
            var id = 10000000;
            var result = (NotFoundObjectResult)await _assetController.GetAssetByIdAsync(id);
            var value = (UnitResult<AssetDto>)result.Value;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.True(value.EndOnError);
            Assert.False(value.EndOnSuccess);
            Assert.False(value.EndOnValidationError);
            Assert.Null(value.Result);
        }

        [Fact]
        public async Task GetAssetByIdShouldReturnError()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.AssetRepository.GetByIdAsync(It.IsAny<int>())).Throws(new Exception());
            var assetService = new AssetService(_validatorService, _mapperService, mockUnitOfWork.Object, _logger);
            var assetController = new Assetcontroller(assetService);
            var id = 1;

            var result = (ObjectResult)await assetController.GetAssetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task UpdateAssetShouldReturnOK()
        {
            var asset = new AssetUpdateRequestExample().GetExamples();

            var result = (ObjectResult)await _assetController.UpdateAssetAsync(asset);
            var value = (UnitResult<AssetDto>)result.Value;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.True(value.EndOnSuccess);
            Assert.False(value.EndOnError);
            Assert.False(value.EndOnValidationError);
            Assert.NotNull(value.Result);
            Assert.Equal(asset.ID, value.Result.ID);
            Assert.Equal(asset.AssetName, value.Result.AssetName);
        }

        [Fact]
        public async Task UpdateAssetShouldReturnNotFound()
        {
            var asset = new AssetUpdateRequestExample().GetExamples();
            asset.ID = 10000000;

            var result = (NotFoundObjectResult)await _assetController.UpdateAssetAsync(asset);
            var value = (UnitResult<AssetDto>)result.Value;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.True(value.EndOnError);
            Assert.False(value.EndOnSuccess);
            Assert.False(value.EndOnValidationError);
            Assert.Null(value.Result);
        }

        [Fact]
        public async Task UpdateAssetShouldReturnError()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.AssetRepository.GetByIdAsync(It.IsAny<int>())).Throws(new Exception());
            var assetService = new AssetService(_validatorService, _mapperService, mockUnitOfWork.Object, _logger);
            var assetController = new Assetcontroller(assetService);
            var asset = new AssetUpdateRequestExample().GetExamples();

            var result = (ObjectResult)await assetController.UpdateAssetAsync(asset);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task UpdateAssetShouldReturnBadRequestError()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.AssetRepository.GetByIdAsync(It.IsAny<int>())).Throws(new Exception());
            var assetService = new AssetService(_validatorService, _mapperService, mockUnitOfWork.Object, _logger);
            var assetController = new Assetcontroller(assetService);
            var asset = new AssetCreationUpdateBadRequestExample().GetExamples();

            var result = (BadRequestObjectResult)await assetController.UpdateAssetAsync(asset);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task DeleteAssetShouldReturnOK()
        {
            var id = 4;

            var result = (ObjectResult)await _assetController.DeleteAssetAsync(id);
            var value = (UnitResult<AssetDto>)result.Value;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.True(value.EndOnSuccess);
        }

        [Fact]
        public async Task DeleteAssetShouldReturnNotFound()
        {
            var id = 100000;

            var result = (NotFoundObjectResult)await _assetController.DeleteAssetAsync(id);
            var value = (UnitResult<AssetDto>)result.Value;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.True(value.EndOnError);
            Assert.False(value.EndOnSuccess);
        }

        [Fact]
        public async Task DeleteAssetShouldReturnError()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.AssetRepository.GetByIdAsync(It.IsAny<int>())).Throws(new Exception());
            var assetService = new AssetService(_validatorService, _mapperService, mockUnitOfWork.Object, _logger);
            var assetController = new Assetcontroller(assetService);
            var id = 1;

            var result = (ObjectResult)await assetController.DeleteAssetAsync(id);
            var value = (UnitResult<AssetDto>)result.Value;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
            Assert.True(value.EndOnError);
            Assert.False(value.EndOnSuccess);
        }
    }
}
