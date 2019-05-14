using FluentValidation.Attributes;
using RedisStackOverflow.Entities.Locations.Validations;
using RedisStackOverflow.Entities.Redis;

namespace RedisStackOverflow.Entities
{
    [Validator(typeof(CountryValidator))]
    public class Country : RedisDefaultKey<Country, CountryValidator>
    {
        //public override ulong Id { get; set; }
        public string Name { get; set; }
        public string Initials { get; set; }
    }
}
