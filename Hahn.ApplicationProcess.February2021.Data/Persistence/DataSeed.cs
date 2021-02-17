using Hahn.ApplicationProcess.February2021.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Hahn.ApplicationProcess.February2021.Data.Persistence
{
    public static class DataSeed
    {
        public static void Initialize(DbContextOptions<ApplicationDbContext> dbContextOpt)
        {
            using (var context = new ApplicationDbContext(dbContextOpt))
            {
                if (context.Assets.Any())
                {
                    return;
                }

                context.Assets.AddRange(
                    new Asset
                    {
                        ID = 1,
                        AssetName = "Asset1",
                        CountryOfDepartment = "Colombia",
                        DepartmentId = 1,
                        EMailAdressOfDepartment = "Asset1@asset.com",
                        IsBroken = false,
                        PurchaseDate = DateTimeOffset.Parse("01/01/2019"),
                    },
                    new Asset
                    {
                        ID = 2,
                        AssetName = "Asset2",
                        CountryOfDepartment = "Chile",
                        DepartmentId = 2,
                        EMailAdressOfDepartment = "Asset2@asset.com",
                        IsBroken = false,
                        PurchaseDate = DateTimeOffset.Parse("02/02/2019"),
                    },
                    new Asset
                    {
                        ID = 3,
                        AssetName = "Asset3",
                        CountryOfDepartment = "Estados Unidos",
                        DepartmentId = 3,
                        EMailAdressOfDepartment = "Asset3@asset.com",
                        IsBroken = true,
                        PurchaseDate = DateTimeOffset.Parse("03/03/2019"),
                    },
                    new Asset
                    {
                        ID = 4,
                        AssetName = "Asset4",
                        CountryOfDepartment = "Argentina",
                        DepartmentId = 4,
                        EMailAdressOfDepartment = "Asset4@asset.com",
                        IsBroken = true,
                        PurchaseDate = DateTimeOffset.Parse("04/04/2020"),
                    },
                    new Asset
                    {
                        ID = 5,
                        AssetName = "Asset5",
                        CountryOfDepartment = "Mexico",
                        DepartmentId = 1,
                        EMailAdressOfDepartment = "Asset5@asset.com",
                        IsBroken = false,
                        PurchaseDate = DateTimeOffset.Parse("05/05/2020"),
                    });

                context.SaveChanges();
            }
        }
    }
}
