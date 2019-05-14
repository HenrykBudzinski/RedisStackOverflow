using FluentValidation.Attributes;
using RedisStackOverflow.Entities.Locations.Validations;
using RedisStackOverflow.Entities.Redis;

namespace RedisStackOverflow.Entities
{
    [Validator(typeof(StateValidator))]
    public class State : RedisDefaultKey<State, StateValidator>
    {
        public string Name { get; set; }
        public string Initials { get; set; }
        public ulong CountryId { get; set; }
    }
}
