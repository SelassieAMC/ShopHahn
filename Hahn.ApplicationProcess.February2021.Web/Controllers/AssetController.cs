using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using Hahn.ApplicationProcess.February2021.Domain.ResponseObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Web.Controllers
{
    /// <summary>
    /// Asset controller
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class Assetcontroller : Controller
    {
        private readonly IAssetService _assetService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="assetService"></param>
        public Assetcontroller(IAssetService assetService)
        {
            _assetService = assetService;
        }

        /// <summary>
        /// Create an Asset from an object
        /// </summary>
        /// <param name="asset">Asset object to create</param>
        /// <returns>Asset object created</returns>
        [HttpPost]
        [ProducesResponseType(typeof(UnitResult<AssetDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(UnitResult<AssetDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAssetAsync(AssetDto asset)
        {
            var result = await _assetService.CreateAsset(asset);
            if (result.EndOnSuccess)
            {
                return StatusCode(StatusCodes.Status201Created, result.Result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
        }

        /// <summary>
        /// Fetched an Asset by the given Id
        /// </summary>
        /// <param name="id">Asset Id</param>
        /// <returns>Asset object with the Id specified</returns>
        [HttpGet]
        [ProducesResponseType(typeof(UnitResult<AssetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnitResult<AssetDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UnitResult<AssetDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAssetByIdAsync([FromQuery] int id)
        {
            var result = await _assetService.GetAssetById(id);
            if (result.Result is null)
            {
                return NotFound();
            }
            if (result.EndOnSuccess)
            {
                return Ok(result.Result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
        }

        /// <summary>
        /// Update an Asset with the given Id and new data
        /// </summary>
        /// <param name="id">Asset Id</param>
        /// <param name="asset">Asset object with the new data</param>
        /// <returns>Asset object with the new data</returns>
        [HttpPut]
        [ProducesResponseType(typeof(UnitResult<AssetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnitResult<AssetDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UnitResult<AssetDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAssetAsync([FromQuery] int id, AssetDto asset)
        {
            var result = await _assetService.UpdateAsset(id, asset);
            if (result.Result is null)
            {
                return NotFound();
            }
            if (result.EndOnSuccess)
            {
                return Ok(result.Result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
        }

        /// <summary>
        /// Delete an Asset by the given Id 
        /// </summary>
        /// <param name="id">Asset Id</param>
        /// <returns>StatusResult</returns>
        [HttpDelete]
        [ProducesResponseType(typeof(UnitResult<AssetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnitResult<AssetDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UnitResult<AssetDto>), StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAssetAsync([FromQuery] int id)
        {
            var result = _assetService.DeleteAsset(id);
            if (result.EndOnSuccess)
            {
                return Ok();
            }
            if (result.EndOnError)
            {
                return NotFound(result.ErrorMessage);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
        }
    }
}
