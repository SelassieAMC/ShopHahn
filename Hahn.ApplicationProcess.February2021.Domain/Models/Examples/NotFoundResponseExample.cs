using Hahn.ApplicationProcess.February2021.Domain.ResponseObjects;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicationProcess.February2021.Domain.Models.Examples
{
    public class NotFoundResponseExample : IExamplesProvider<UnitResult<object>>
    {
        public UnitResult<object> GetExamples()
        {
            return new UnitResult<object>()
            {
                Result = null,
                EndOnError = true,
                ErrorMessage = "Asset not found!"
            };
        }
    }
}
