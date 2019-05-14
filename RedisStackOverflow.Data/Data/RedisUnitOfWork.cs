using RedisStackOverflow.Data.Utils;
using RedisStackOverflow.Entities;
using RedisStackOverflow.Entities.Locations.Validations;
using RedisStackOverflowTest.Data.Repositories.Locations;
using StackExchange.Redis;
using System;

namespace RedisStackOverflow.Data
{
    public sealed class RedisUnitOfWork : IDisposable
    {
        private bool _disposed = false;
        private ConnectionMultiplexer _redisConnection;
        public readonly CountryRepository Countries;

        public RedisUnitOfWork()
        {
            _redisConnection = ConnectionMultiplexer.Connect("localhost");
            Countries = 
                new CountryRepository(
                    _redisConnection.GetDatabase(), 
                    new ReflectionHelper<Country>(),
                    new RedisEntityHelper<Country,CountryValidator>());
        }

        #region IDispose
        public void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if(disposing)
            {
                _redisConnection.Dispose();
            }

            _redisConnection = null;
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~RedisUnitOfWork()
        {
            Dispose(false);
        }
        #endregion
    }

}
