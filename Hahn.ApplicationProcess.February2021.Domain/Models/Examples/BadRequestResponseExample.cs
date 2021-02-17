using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.ResponseObjects;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicationProcess.February2021.Domain.Models.Examples
{
    public class BadRequestResponseExample : IExamplesProvider<UnitResult<AssetDto>>
    {
        public UnitResult<AssetDto> GetExamples()
    {
        return new UnitResult<AssetDto>()
        {
            Result = null,
            EndOnValidationError = true,
            ErrorMessage = "Invalidad value at property AssetName, it must contain at least 5 characters."
        };
    }
}
}
