using FluentValidation;
using FluentValidation.Validators;
using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Validators
{
    public class AssetValidator : AbstractValidator<AssetDto>
    {
        public AssetValidator(ISearchCountry searchCountry)
        {
            var message = string.Empty;

            RuleFor(asset => asset.AssetName)
                .Length(5, 255)
                .WithMessage("The name must contain at least 5 characters.");
            RuleFor(asset => asset.Deparment)
                .IsInEnum()
                .WithMessage("The department type is invalid.");
            RuleFor(asset => asset.PurchaseDate)
                .GreaterThanOrEqualTo(DateTimeOffset.UtcNow.AddYears(-1))
                .WithMessage("Purchase Order must not be olden than one year");
            RuleFor(asset => asset.EMailAdressOfDepartment)
                .EmailAddress();
            RuleFor(asset => asset.CountryOfDepartment)
                .MustAsync(async (country, cancellationToken) =>
               {
                   var result = await searchCountry.SearchAsync(country);
                   message = result.Item1;
                   return result.Item2;
               }).WithMessage(message);
        }
    }
}
