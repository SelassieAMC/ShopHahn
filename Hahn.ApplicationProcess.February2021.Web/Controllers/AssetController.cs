using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using Hahn.ApplicationProcess.February2021.Domain.Models.Examples;
using Hahn.ApplicationProcess.February2021.Domain.ResponseObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
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
        /// <response code="201">Returns the newly created assset</response>
        /// <response code="400">Validation error in the given Asset</response> 
        /// <response code="500">Any server side error</response>  
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, type: typeof(AssetCreationResponseExample), description: "Successfully added the Asset")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(BadRequestResponseExample), description: "Validation errors")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(InternalServerErrorResponseExample), description: "Server errors")]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(AssetCreationResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorResponseExample))]
        [SwaggerRequestExample(typeof(AssetDto), typeof(AssetCreationRequestExample))]
        public async Task<IActionResult> CreateAssetAsync(AssetDto asset)
        {
            var result = await _assetService.CreateAsset(asset);
            if (result.EndOnSuccess)
            {
                return StatusCode(StatusCodes.Status201Created, result);
            }
            if (result.EndOnValidationError)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Fetched an Asset by the given Id
        /// </summary>
        /// <param name="id">Asset Id</param>
        /// <response code="200">Found the asset by the given id</response> 
        /// <response code="404">Asset not found for update</response> 
        /// <response code="500">Any server side error</response>  
        [HttpGet()]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(AssetGetResponseExample), description: "Found the Asset by the given Id")]
        [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(NotFoundResponseExample), description: "Asset not found!")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(InternalServerErrorResponseExample), description: "Server errors")]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AssetGetResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorResponseExample))]
        public async Task<IActionResult> GetAssetByIdAsync([FromQuery] int id)
        {
            var result = await _assetService.GetAssetById(id);
            if (result.Result is null)
            {
                return NotFound(result);
            }
            if (result.EndOnSuccess)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update an Asset with the given Id and new data
        /// </summary>
        /// <param name="asset">Asset object with the new data</param>
        /// <returns>Asset object with the new data</returns>
        /// <response code="200">Successfully updated the Asset</response>
        /// <response code="400">Validation error in the given Asset</response> 
        /// <response code="404">Asset not found for update</response> 
        /// <response code="500">Any server side error</response>  
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(AssetUpdateResponseExample), description: "Successfully updated the Asset")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(BadRequestResponseExample), description: "Validation errors")]
        [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(NotFoundResponseExample), description: "Asset not found!")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(InternalServerErrorResponseExample), description: "Server errors")]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AssetUpdateResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorResponseExample))]
        [SwaggerRequestExample(typeof(AssetDto), typeof(AssetUpdateRequestExample))]
        public async Task<IActionResult> UpdateAssetAsync(AssetDto asset)
        {
            var result = await _assetService.UpdateAsset(asset);
            if (result.EndOnValidationError)
            {
                return BadRequest(result);
            }
            if (result.Result is null)
            {
                return NotFound(result);
            }
            if (result.EndOnSuccess)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete an Asset by the given Id 
        /// </summary>
        /// <param name="id">Asset Id</param>
        /// <response code="200">Successfully deleted the Asset</response>
        /// <response code="404">Asset not found for update</response> 
        /// <response code="500">Any server side error</response> 
        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(AssetDeleteResponseExample))]
        [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(NotFoundResponseExample))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type:typeof(InternalServerErrorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AssetDeleteResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorResponseExample))]
        public async Task<IActionResult> DeleteAssetAsync([FromQuery] int id)
        {
            var result = await _assetService.DeleteAsset(id);
            if (result.EndOnSuccess)
            {
                return Ok(result);
            }
            if (result.Result is null)
            {
                return NotFound(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}
