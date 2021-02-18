using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.ResponseObjects;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicationProcess.February2021.Domain.Models.Examples
{
    public class AssetDeleteResponseExample : IExamplesProvider<UnitResult<AssetDto>>
    {
        public UnitResult<AssetDto> GetExamples()
        {
            return new UnitResult<AssetDto>()
            {
                Result = null,
                EndOnSuccess = true
            };
        }
    }
}
