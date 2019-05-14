using FluentValidation.Attributes;
using RedisStackOverflow.Entities.Entities.Locations.Validations;
using RedisStackOverflow.Entities.Redis;

namespace RedisStackOverflow.Entities
{
    [Validator(typeof(CountryValidator))]
    public class Country : IRedisKey
    {
        public uint Id { get; set; }
        public string Nome { get; set; }
        public string Initials { get; set; }

        public string GetRedisKey()
        {
            return "country:" + Id;
        }
    }
}
