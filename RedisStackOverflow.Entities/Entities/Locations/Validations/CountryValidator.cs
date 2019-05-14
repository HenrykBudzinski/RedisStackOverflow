using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisStackOverflow.Entities.Entities.Locations.Validations
{
    public class CountryValidator : AbstractValidator<Country>
    {
        public CountryValidator()
        {
            RuleFor(o => o.Nome)
                .NotEmpty()
                .WithMessage("O nome do país é obrigatório");

            RuleFor(o => o.Initials)
                .NotEmpty()
                .WithMessage("As iniciais do país é obrigatório");
        }
    }
}
