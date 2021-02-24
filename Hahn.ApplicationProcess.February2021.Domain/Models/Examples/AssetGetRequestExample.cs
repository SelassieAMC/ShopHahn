using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.ResponseObjects;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace Hahn.ApplicationProcess.February2021.Domain.Models.Examples
{
    public class AssetGetResponseExample : IExamplesProvider<UnitResult<AssetDto>>
    {
        public UnitResult<AssetDto> GetExamples()
        {
            return new UnitResult<AssetDto>()
            {
                EndOnSuccess = true,
                Result = new AssetDto()
                {
                    ID = 1,
                    AssetName = "Asset1",
                    CountryOfDepartment = "Colombia",
                    Department = Enums.DepartmentType.HQ,
                    EMailAdressOfDepartment = "Asset1@asset.com",
                    IsBroken = false,
                    PurchaseDate = DateTimeOffset.Parse("01/01/2019")
                }
            };
        }
    }
}