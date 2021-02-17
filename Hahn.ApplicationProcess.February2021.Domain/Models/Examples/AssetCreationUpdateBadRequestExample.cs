using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace Hahn.ApplicationProcess.February2021.Domain.Models.Examples
{
    public class AssetCreationUpdateBadRequestExample : IExamplesProvider<AssetDto>
    {
        public AssetDto GetExamples()
        {
            return new AssetDto()
            {
                AssetName = "err",
                CountryOfDepartment = "Colombia",
                Deparment = Enums.DepartmentType.HQ,
                EMailAdressOfDepartment = "Asset100@asset.com",
                IsBroken = false,
                PurchaseDate = DateTimeOffset.Parse("01/01/2021")
            };
        }
    }
}
