using RedisStackOverflowTest.Data.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisStackOverflow.Data
{
    public static class RedisUnitOfWork
    {
        private static readonly ConnectionMultiplexer RedisConnection;
        public static readonly CountryRepository Countries;

        static RedisUnitOfWork()
        {
            RedisConnection = ConnectionMultiplexer.Connect("localhost");
            Countries = new CountryRepository(RedisConnection.GetDatabase());
        }

    }
}
