using FluentValidation.Attributes;
using RedisStackOverflow.Entities.Locations.Validations;
using RedisStackOverflow.Entities.Redis;

namespace RedisStackOverflow.Entities
{
    [Validator(typeof(CityValidator))]
    public class City : RedisDefaultKey<City, CityValidator>
    {
        public City()
        {
            Size = new LocationSize(32, 32);
        }

        //public override ulong Id { get; set; }
        public string Name { get; set; }
        public byte CityTypeId { get; set; }
        public ulong StateId { get; set; }
        public LocationSize Size { get; set; }
    }
}
