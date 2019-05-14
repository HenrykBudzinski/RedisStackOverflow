namespace RedisStackOverflow.Entities.Redis
{
    public interface IRedisDefaultKey<TKeySuffix>
    {
        string GetRedisKey();
        //void SetRedisKeySuffix(TKeySuffix id);
    }
}
