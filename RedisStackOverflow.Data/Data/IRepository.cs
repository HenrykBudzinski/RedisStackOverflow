using RedisStackOverflow.Entities.Redis;

namespace RedisStackOverflow.Data
{
    public interface IRepository<T>
        where T : IRedisKey
    {
        T Get(string key);
        T Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
