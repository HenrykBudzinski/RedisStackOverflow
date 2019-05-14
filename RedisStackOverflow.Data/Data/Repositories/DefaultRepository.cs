using FluentValidation;
using RedisStackOverflow.Data.Data.Repositories.Exceptions;
using RedisStackOverflow.Data.Utils;
using RedisStackOverflow.Entities.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisStackOverflow.Data.Repositories
{
    public abstract class DefaultRepository<TEntity, TValidator> 
        : IRedisDefaultRepository<TEntity, TValidator>
        where TEntity : RedisDefaultKey<TEntity, TValidator>, new()
        where TValidator : AbstractValidator<TEntity>, new()
    {
        protected IDatabase _db;
        protected ReflectionHelper<TEntity> _reflectorHelper;
        protected RedisEntityHelper<TEntity, TValidator> _redisHelper;

        public DefaultRepository(
            IDatabase db,
            ReflectionHelper<TEntity> reflectorHelper,
            RedisEntityHelper<TEntity, TValidator> redisHelper)
        {
            _db = db;
            _reflectorHelper = reflectorHelper;
            _redisHelper = redisHelper;
        }

        public virtual string GetEntityKey(ulong id)
        {
            return _redisHelper.GetEntityKey(id);
        }

        public virtual ulong CurrentKeySuffix()
        {
            var id = _db.StringGet(typeof(TEntity).Name);
            return
                (id.HasValue)
                    ? Convert.ToUInt64(id)
                    : 1;
        }
        public virtual bool Exists(TEntity entity)
        {
            if (entity.Id < 1)
            {
                throw new Exception(
                    "O identitificador da entidade deve ser maior que zero.");
            }

            var exists = 
                _db.HashGet(
                    entity.GetRedisKey(),
                    _reflectorHelper.GetPropertyName(o => o.Id));

            return exists.HasValue;
        }

        public virtual TEntity Add(TEntity entity)
        {
            var validator = new TValidator();
            validator.ValidateAndThrow(entity);

            var key = entity.GetRedisKey();
            if (entity.GetRedisKey() != null)
            {
                throw new Exception(
                    "Não é possível adicionar uma entidade com identificador definido.");
            }

            entity.Id = (ulong)_db.StringIncrement(entity.GetType().Name, 1);
            var entries = _redisHelper.GetHashSets(entity);
            _db.HashSet(entity.GetRedisKey(), entries);
            return entity;
        }

        public virtual TEntity Get(string key)
        {
            if (!_db.KeyExists(key))
            {
                return null;
            }

            var entries = _db.HashGetAll(key);
            return _redisHelper.GetEntity(entries);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            //  Usar Redis.Multi Redis.Exec ou Redis.Discard
            var transaction = _db.CreateTransaction();

            return null;
        }

        public virtual void Delete(TEntity entity)
        {
            var validator = new TValidator();
            validator.ValidateAndThrow(entity);

            if (!Exists(entity))
            {
                throw new Exception(
                    "A entidade informada não existe.");
            }
            var fields = _db.HashKeys(entity.GetRedisKey());
            if(fields.Any())
            {
                var deletedCount = _db.HashDelete(entity.GetRedisKey(), fields);
                var entityIsDeleted = _db.KeyDelete(entity.GetRedisKey());

                if(!entityIsDeleted)
                {
                    throw new RedisKeyNotDeletedException(entity.GetRedisKey());
                }
            }
        }

        public virtual void Update(TEntity entity)
        {
            var validator = new TValidator();
            validator.ValidateAndThrow(entity);

            if (!Exists(entity))
            {
                throw new Exception(
                    "A entidade informada não existe.");
            }
            var entries = _redisHelper.GetHashSets(entity);
            _db.HashSet(entity.GetRedisKey(), entries);
        }
    }
}
