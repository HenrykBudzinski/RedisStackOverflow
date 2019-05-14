using RedisStackOverflow.Entities.Redis;

namespace RedisStackOverflow.Entities
{
    public class State : IRedisKey
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Initials { get; set; }
        public uint CountryId { get; set; }

        public string GetRedisKey()
        {
            return "state" + CountryId + ":" + Id;
        }
    }
}
