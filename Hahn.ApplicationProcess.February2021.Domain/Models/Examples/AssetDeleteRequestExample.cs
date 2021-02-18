using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicationProcess.February2021.Domain.Models.Examples
{
    public class AssetDeleteRequestExample : IExamplesProvider<int>
    {
        public int GetExamples()
        {
            return 4;
        }
    }
}
