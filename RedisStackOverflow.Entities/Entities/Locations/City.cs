using RedisStackOverflow.Entities.Redis;

namespace RedisStackOverflow.Entities
{
    public class City : IRedisKey
    {
        public City()
        {
            Size = new LocationSize(32, 32);
        }

        public uint Id { get; set; }
        public string Name { get; set; }
        public byte CityTypeId { get; set; }
        public uint StateId { get; set; }
        public LocationSize Size { get; set; }

        public string GetRedisKey()
        {
            return "city" + StateId + ":" + Id;
        }
    }
}
