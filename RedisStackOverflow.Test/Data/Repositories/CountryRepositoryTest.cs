using NUnit.Framework;
using RedisStackOverflow.Data;
using RedisStackOverflow.Data.Utils;
using RedisStackOverflow.Entities;

namespace RedisStackOverflow.Test.Data.Repositories
{
    [TestFixture]
    public class CountryRepositoryTest
    {
        private RedisUnitOfWork _unit;

        [OneTimeSetUp]
        public void SetUp()
        {
            _unit = new RedisUnitOfWork();
        }

        [Test, Order(1)]
        public void CountryRepository_AddCuntry_ReturnTrue()
        {
            var repo = _unit.Countries;
            var country = new Country
            {
                Name = "Brasil",
                Initials = "BR"
            };

            var currentId = repo.CurrentKeySuffix();
            var addCountry = repo.Add(country);

            Assert.That(
                (currentId == 1)
                    ? addCountry.Id == 1
                    : addCountry.Id == currentId + 1,
                Is.True);
        }

        [Test, Order(2)]
        public void CountryRepository_GetAndUpdateCuntry_ReturnTrue()
        {
            var repo = _unit.Countries;

            var currentId = repo.CurrentKeySuffix();

            var country =
                repo.Get(
                    repo.GetEntityKey(
                        currentId));

            Assert.That(country.Id == currentId, Is.True);

            var updatedCountry = new Country
            {
                Id = currentId,
                Name = "United States of America",
                Initials = "USA"
            };
            

            repo.Update(updatedCountry);

            updatedCountry =
                repo.Get(
                    repo.GetEntityKey(
                        currentId));

            Assert.That(
                updatedCountry.Id == country.Id
                && updatedCountry.Name != country.Name
                && updatedCountry.Initials != country.Initials,
                Is.True
            );
        }

        [Test, Order(3)]
        public void CountryRepository_DeleteCuntry_ReturnTrue()
        {
            var repo = _unit.Countries;

            var currentId = repo.CurrentKeySuffix();
            var country =
                repo.Get(
                    repo.GetEntityKey(
                        currentId));

            Assert.That(country.Id == currentId, Is.True);

            repo.Delete(country);

            country =
                repo.Get(
                    repo.GetEntityKey(
                        currentId));

            Assert.That(country, Is.Null);
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            _unit.Dispose();
        }
    }
}
