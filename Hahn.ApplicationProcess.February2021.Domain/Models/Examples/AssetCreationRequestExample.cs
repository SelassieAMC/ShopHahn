using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace Hahn.ApplicationProcess.February2021.Domain.Models.Examples
{
    public class AssetCreationRequestExample : IExamplesProvider<AssetDto>
    {
        public AssetDto GetExamples()
        {
            return new AssetDto()
            {
                AssetName = "Asset100",
                CountryOfDepartment = "Colombia",
                Deparment = (int)Enums.DepartmentType.HQ,
                EMailAdressOfDepartment = "Asset100@asset.com",
                IsBroken = false,
                PurchaseDate = DateTimeOffset.Parse("01/01/2021")
            };
        }
    }
}
