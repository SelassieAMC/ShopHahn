using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.ResponseObjects;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace Hahn.ApplicationProcess.February2021.Domain.Models.Examples
{
    public class AssetCreationResponseExample : IExamplesProvider<UnitResult<AssetDto>>
    {
        public UnitResult<AssetDto> GetExamples()
        {
            return new UnitResult<AssetDto>()
            {
                Result = new AssetDto()
                {
                    ID = 6,
                    AssetName = "Asset100",
                    CountryOfDepartment = "Colombia",
                    Deparment = Enums.DepartmentType.HQ,
                    EMailAdressOfDepartment = "Asset100@asset.com",
                    IsBroken = false,
                    PurchaseDate = DateTimeOffset.Parse("01/01/2021")
                },
                EndOnSuccess = true
            };
        }
    }
}
