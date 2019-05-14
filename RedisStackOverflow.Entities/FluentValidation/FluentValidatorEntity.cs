using FluentValidation;

namespace RedisStackOverflow.Entities.FluentValidation
{
    public abstract class FluentValidatorEntity<TEntity, TValidator>
        : IFluentValidatorEntity<TEntity, TValidator>
        where TValidator : AbstractValidator<TEntity>
    {
        public abstract IValidator<TEntity> GetValidator();
    }
}
