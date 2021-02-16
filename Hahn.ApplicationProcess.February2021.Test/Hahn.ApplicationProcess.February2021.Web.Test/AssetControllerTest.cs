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

        }

        [Fact]
        public async Task GetAssetByIdShouldReturnNotFound()
        {

        }

        [Fact]
        public async Task GetAssetByIdShouldReturnError()
        {

        }

        [Fact]
        public async Task UpdateAssetShouldReturnOK()
        {

        }

        [Fact]
        public async Task UpdateAssetShouldReturnNotFound()
        {

        }

        [Fact]
        public async Task UpdateAssetShouldReturnError()
        {

        }

        [Fact]
        public async Task DeleteAssetShouldReturnOK()
        {

        }

        [Fact]
        public async Task DeleteAssetShouldReturnNotFound()
        {

        }

        [Fact]
        public async Task DeleteAssetShouldReturnError()
        {

        }
    }
}
