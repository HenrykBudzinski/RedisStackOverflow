using FluentValidation;

namespace RedisStackOverflow.Entities.Locations.Validations
{
    public class CountryValidator : AbstractValidator<Country>
    {
        public CountryValidator()
        {
            RuleFor(o => o.Name)
                .NotEmpty()
                .WithMessage("O nome do país é obrigatório");

            RuleFor(o => o.Initials)
                .NotEmpty()
                .WithMessage("As iniciais do país é obrigatório");
        }
    }
}
