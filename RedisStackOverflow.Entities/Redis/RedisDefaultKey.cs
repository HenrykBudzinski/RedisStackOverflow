using FluentValidation;
using RedisStackOverflow.Entities.FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisStackOverflow.Entities.Redis
{
    public abstract class RedisDefaultKey<TEntity, TValidator> 
        : IFluentValidatorEntity<TEntity, TValidator>, 
            IRedisDefaultKey<ulong>
        where TValidator : AbstractValidator<TEntity>
    {
        public ulong Id { get; set; }

        public virtual string GetRedisKey()
        {
            return
                (Id < 1)
                    ? null
                    : typeof(TEntity).Name + ":" + Id;
        }

        public IValidator<TEntity> GetValidator()
        {
            throw new NotImplementedException();
        }

        //public void SetRedisKeySuffix(ulong id)
        //{
        //    Id = id;
        //}
    }
}
