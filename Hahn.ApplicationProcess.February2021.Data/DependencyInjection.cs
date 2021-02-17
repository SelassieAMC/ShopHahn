using FluentValidation;
using Hahn.ApplicationProcess.February2021.Data.Persistence;
using Hahn.ApplicationProcess.February2021.Data.Persistence.Repositories;
using Hahn.ApplicationProcess.February2021.Data.Persistence.UnitOfWork;
using Hahn.ApplicationProcess.February2021.Data.WebApi;
using Hahn.ApplicationProcess.February2021.Domain.BL;
using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using Hahn.ApplicationProcess.February2021.Domain.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Hahn.ApplicationProcess.February2021.Data
{
    public static class DependencyInjection
    {
        
        public static void AddAppServices(this IServiceCollection services)
        {
            var countryApiEndpoint = "https://restcountries.eu/rest/v2/name/";

            services.AddHttpClient("country", c => c.BaseAddress = new Uri(countryApiEndpoint));
            services.AddSingleton<ISearchCountry, SearchCountry>();
            services.AddSingleton<IValidator<AssetDto>, AssetValidator>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IAssetService, AssetService>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(databaseName: "HahnApplicationProcess"));

            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            var contextOptions = services.BuildServiceProvider().GetRequiredService<DbContextOptions<ApplicationDbContext>>();
            DataSeed.Initialize(contextOptions);
        }
    }
}
