using NUnit.Framework;
using RedisStackOverflow.Data;
using RedisStackOverflow.Entities;
using RedisStackOverflowTest.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisStackOverflow.Test.Data.Repositories
{
    [TestFixture]
    public class CountryRepositoryTest
    {
        [Test]
        public void AddCuntry_IncrementId_ReturnTrue()
        {
            var repo = RedisUnitOfWork.Countries;
            var country = new Country() { Id = 1 };
            var getCountry = repo.Get(country.GetRedisKey());

            country = new Country
            {
                Nome = "Brasil",
                Initials = "BR"
            };

            var addCountry = repo.Add(country);

            Assert.That(country.Id > 0, Is.True);

            country = new Country
            {
                Nome = "Brasil",
                Initials = "BR"
            };
        }
    }
}
