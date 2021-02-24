using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.ResponseObjects;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace Hahn.ApplicationProcess.February2021.Domain.Models.Examples
{
    public class AssetUpdateResponseExample : IExamplesProvider<UnitResult<AssetDto>>
    {
        public UnitResult<AssetDto> GetExamples()
        {
            return new UnitResult<AssetDto>(){
                Result = new AssetDto()
                {
                    ID = 1,
                    AssetName = "AssetEdited",
                    CountryOfDepartment = "Venezuela",
                    Deparment = (int)Enums.DepartmentType.Store1,
                    EMailAdressOfDepartment = "AssetEdited@asset.com",
                    IsBroken = false,
                    PurchaseDate = DateTimeOffset.Parse("01/02/2021")
                },
                EndOnSuccess = true
            };
        }
    }
}
