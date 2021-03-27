using FluentValidation;

namespace SGM.SAC.Api.Models.Validators
{
    public class PropertyTaxRequestValidator : AbstractValidator<PropertyTaxRequest>
    {
        public PropertyTaxRequestValidator()
        {
            RuleFor(r => r.PropertyRegistration)
                .NotEmpty()                
                .WithMessage("Invalid PropertyRegistration value, should not be null or empty.");

            RuleFor(r => r.IsRuralTax)             
              .NotNull()
              .WithMessage("Invalid IsRuralTax flag value, should not be null.");
        }
    }
}
