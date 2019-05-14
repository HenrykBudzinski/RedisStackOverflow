using FluentValidation;
using RedisStackOverflow.Entities.Redis;
using System.Collections.Generic;

namespace RedisStackOverflow.Data.Repositories
{
    public interface IRedisDefaultRepository<TEntity, TValidator>
        where TEntity : RedisDefaultKey<TEntity, TValidator>, new()
        where TValidator : AbstractValidator<TEntity>
    {
        ulong CurrentKeySuffix();
        TEntity Get(string key);
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
