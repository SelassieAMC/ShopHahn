using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace Hahn.ApplicationProcess.February2021.Domain.Models.Examples
{
    public class AssetUpdateRequestExample : IExamplesProvider<AssetDto>
    {
        public AssetDto GetExamples()
        {
            return new AssetDto()
            {
                ID = 1,
                AssetName = "AssetEdited",
                CountryOfDepartment = "Argentina",
                Deparment = Enums.DepartmentType.Store1,
                EMailAdressOfDepartment = "AssetEdited@asset.com",
                IsBroken = false,
                PurchaseDate = DateTimeOffset.Parse("01/02/2021")
            };
        }
    }
}
