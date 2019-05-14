using FluentValidation;

namespace RedisStackOverflow.Entities.Locations.Validations
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(o => o.CityTypeId)
                .NotEmpty()
                .WithMessage("O tipo da cidade deve ser informado.");

            RuleFor(o => o.Name)
                .NotEmpty()
                .WithMessage("O nome da cidade deve ser informado.")
                .Length(1,80)
                .WithMessage(
                    "A quantidade de caracteres no nome da cidade é de no minimo {MinLength} "
                    + "e no máximo {MaxLength}. Caracteres informados {TotalLength}.");

            RuleFor(o => o.Size)
                .NotEmpty()
                .WithMessage("O tamanho da cidade deve ser informado.");

            RuleFor(o => o.StateId)
                .Must(id => id > 0)
                .WithMessage("O tamanho da cidade deve ser informado.");
        }
    }
}
