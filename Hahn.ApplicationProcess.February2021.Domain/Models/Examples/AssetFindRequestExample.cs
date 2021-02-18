using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicationProcess.February2021.Domain.Models.Examples
{
    public class AssetFindRequestExample : IExamplesProvider<int>
    {
        public int GetExamples()
        {
            return 1;
        }
    }
}
