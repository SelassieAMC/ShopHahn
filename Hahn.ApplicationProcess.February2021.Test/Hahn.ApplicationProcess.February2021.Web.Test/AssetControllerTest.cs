using FluentValidation;
using Hahn.ApplicationProcess.February2021.Domain.BL;
using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using Hahn.ApplicationProcess.February2021.Domain.ResponseObjects;
using Hahn.ApplicationProcess.February2021.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hahn.ApplicationProcess.February2021.Test.Hahn.ApplicationProcess.February2021.Web.Test
{
    public class AssetControllerTest
    {
        private readonly Mock<IAssetService> _mockAssetService;
        private readonly Assetcontroller _assetController;

        public AssetControllerTest()
        {
            _mockAssetService = new Mock<IAssetService>();
            _assetController = new Assetcontroller(_mockAssetService.Object);
        }
        [Fact]
        public async Task CreateAssetShouldReturn201Status()
        {
            var asset = new AssetDto();

            _mockAssetService.Setup(m => m.CreateAsset(It.IsAny<AssetDto>())).Returns((AssetDto s) => Task.FromResult(new UnitResult<AssetDto>() { Result = s, EndOnSuccess = true }));
            var result = (ObjectResult) await _assetController.CreateAssetAsync(asset);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        }

        [Fact]
        public async Task CreateAssetShouldReturnError()
        {
            var asset = new AssetDto();
            _mockAssetService.Setup(m => m.CreateAsset(It.IsAny<AssetDto>())).Returns((AssetDto s) => Task.FromResult(new UnitResult<AssetDto>()));
            var result = (ObjectResult) await _assetController.CreateAssetAsync(asset);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetAssetByIdShouldReturnOK()
        {
            var id = 1;
            _mockAssetService.Setup(m => m.GetAssetById(It.IsAny<int>())).Returns(Task.FromResult(new UnitResult<AssetDto>() { EndOnSuccess = true, Result = new AssetDto() }));
            var result = (ObjectResult)await _assetController.GetAssetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public async Task GetAssetByIdShouldReturnNotFound()
        {
            var id = 1;
            _mockAssetService.Setup(m => m.GetAssetById(It.IsAny<int>())).Returns(Task.FromResult(new UnitResult<AssetDto>() { Result = null}));
            var result = (NotFoundResult)await _assetController.GetAssetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GetAssetByIdShouldReturnError()
        {
            var id = 1;
            _mockAssetService.Setup(m => m.GetAssetById(It.IsAny<int>())).Returns(Task.FromResult(new UnitResult<AssetDto>() { Result = new AssetDto(), EndOnError = true}));
            var result = (ObjectResult)await _assetController.GetAssetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task UpdateAssetShouldReturnOK()
        {
            var id = 1;
            var asset = new AssetDto();
            _mockAssetService.Setup(m => m.UpdateAsset(It.IsAny<AssetDto>())).Returns((AssetDto s) => Task.FromResult(new UnitResult<AssetDto>() { EndOnSuccess = true, Result = s }));
            var result = (ObjectResult)await _assetController.UpdateAssetAsync(id, asset);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public async Task UpdateAssetShouldReturnNotFound()
        {
            var id = 1;
            var asset = new AssetDto();
            _mockAssetService.Setup(m => m.UpdateAsset(It.IsAny<AssetDto>())).Returns(Task.FromResult(new UnitResult<AssetDto>() { Result = null }));
            var result = (NotFoundResult)await _assetController.UpdateAssetAsync(id, asset);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task UpdateAssetShouldReturnError()
        {
            var id = 1;
            var asset = new AssetDto();
            _mockAssetService.Setup(m => m.UpdateAsset( It.IsAny<AssetDto>())).Returns(Task.FromResult(new UnitResult<AssetDto>() { Result = new AssetDto(), EndOnError = true }));
            var result = (ObjectResult)await _assetController.UpdateAssetAsync(id, asset);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public void DeleteAssetShouldReturnOK()
        {
            var id = 1;
            _mockAssetService.Setup(m => m.DeleteAsset(It.IsAny<int>())).Returns(new UnitResult<AssetDto>() { EndOnSuccess = true });
            var result = (OkResult) _assetController.DeleteAsset(id);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void DeleteAssetShouldReturnNotFound()
        {
            var id = 1;
            _mockAssetService.Setup(m => m.DeleteAsset(It.IsAny<int>())).Returns(new UnitResult<AssetDto>() { EndOnError = true });
            var result = (NotFoundResult)_assetController.DeleteAsset(id);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public void DeleteAssetShouldReturnError()
        {
            var id = 1;
            _mockAssetService.Setup(m => m.DeleteAsset(It.IsAny<int>())).Returns(new UnitResult<AssetDto>());
            var result = (ObjectResult)_assetController.DeleteAsset(id);

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
    }
}
