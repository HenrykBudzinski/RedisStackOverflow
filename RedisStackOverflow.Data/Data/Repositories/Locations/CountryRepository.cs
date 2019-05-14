using RedisStackOverflow.Data.Repositories;
using RedisStackOverflow.Data.Utils;
using RedisStackOverflow.Entities;
using RedisStackOverflow.Entities.Locations.Validations;
using StackExchange.Redis;
using System;

namespace RedisStackOverflowTest.Data.Repositories.Locations
{
    public class CountryRepository : DefaultRepository<Country, CountryValidator>
    {
        public CountryRepository(
            IDatabase db,
            ReflectionHelper<Country> reflectorHelper,
            RedisEntityHelper<Country, CountryValidator> redisHelper) 
            : base(db, reflectorHelper, redisHelper)
        {
        }
    }
}
