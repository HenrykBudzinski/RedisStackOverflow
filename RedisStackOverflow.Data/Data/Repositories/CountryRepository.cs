using RedisStackOverflow.Data;
using RedisStackOverflow.Entities;
using RedisStackOverflow.Data.Utils;
using StackExchange.Redis;
using System;

namespace RedisStackOverflowTest.Data.Repositories
{
    public class CountryRepository : IRepository<Country>
    {
        private IDatabase _db;

        public CountryRepository(IDatabase db)
        {
            _db = db;
        }

        public Country Add(Country entity)
        {
            if(entity.Id != 0)
            {
                throw new Exception(
                    "Não é possível adicionar uma entidade com identificador definido.");
            }

            entity.Id = (uint)_db.StringIncrement(entity.GetType().Name, 1);
            var entries = entity.GetHashSets();
            _db.HashSet(entity.GetRedisKey(), entries);
            return entity;
        }

        public bool Delete(Country entity)
        {
            throw new NotImplementedException();
        }

        public Country Get(string key)
        {
            throw new NotImplementedException();
        }

        public bool Update(Country entity)
        {
            throw new NotImplementedException();
        }
    }
}
