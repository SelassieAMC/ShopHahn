using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.ResponseObjects;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicationProcess.February2021.Domain.Models.Examples
{
    public class InternalServerErrorResponseExample : IExamplesProvider<UnitResult<object>>
    {
        public UnitResult<object> GetExamples()
        {
            return new UnitResult<object>()
            {
                Result = null,
                EndOnError = true,
                ErrorMessage = "Any internal server error or exception"
            };
        }
    }
}
