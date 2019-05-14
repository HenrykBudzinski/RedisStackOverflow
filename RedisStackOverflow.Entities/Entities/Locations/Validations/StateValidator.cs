using FluentValidation;

namespace RedisStackOverflow.Entities.Locations.Validations
{
    public class StateValidator : AbstractValidator<State>
    {
        public StateValidator()
        {
            RuleFor(o => o.CountryId)
                .Must(id => id > 0)
                .WithMessage("O país deve ser informado.");

            RuleFor(o => o.Initials)
                .Empty()
                .WithMessage("As iniciais do estado devem ser informadas.");

            RuleFor(o => o.Name)
                .Empty()
                .WithMessage("O nome do estado devem ser informadas.")
                .Length(1, 80)
                .WithMessage(
                    "A quantidade de caracteres no nome do estado é de no minimo {MinLength} "
                    + "e no máximo {MaxLength}. Caracteres informados {TotalLength}.");
        }
    }
}
