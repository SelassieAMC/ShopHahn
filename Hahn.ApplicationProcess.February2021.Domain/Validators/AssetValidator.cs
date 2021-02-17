using FluentValidation;
using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using System;

namespace Hahn.ApplicationProcess.February2021.Domain.Validators
{
    public class AssetValidator : AbstractValidator<AssetDto>
    {
        public AssetValidator(ISearchCountry searchCountry)
        {
            RuleFor(asset => asset.AssetName)
                .Length(5, 255)
                .WithMessage("it must contain at least 5 characters");
            RuleFor(asset => asset.Deparment)
                .IsInEnum()
                .WithMessage("department type is invalid.");
            RuleFor(asset => asset.PurchaseDate)
                .GreaterThanOrEqualTo(DateTimeOffset.UtcNow.AddYears(-1))
                .WithMessage("date must not be older than one year");
            RuleFor(asset => asset.EMailAdressOfDepartment)
                .EmailAddress();
            RuleFor(asset => asset.CountryOfDepartment)
                .MustAsync(async (country, cancellationToken) =>
               {
                   var result = await searchCountry.SearchAsync(country);
                   return result;
               }).WithMessage("country is invalid");
        }
    }
}
