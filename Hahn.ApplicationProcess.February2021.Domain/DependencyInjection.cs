using FluentValidation;
using Hahn.ApplicationProcess.February2021.Domain.BL;
using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using Hahn.ApplicationProcess.February2021.Domain.Validators;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Hahn.ApplicationProcess.February2021.Domain
{
    public static class DependencyInjection
    {
        
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IValidator, AssetValidator>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
