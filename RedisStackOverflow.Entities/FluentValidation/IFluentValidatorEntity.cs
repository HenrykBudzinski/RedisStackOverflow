using FluentValidation;

namespace RedisStackOverflow.Entities.FluentValidation
{
    public interface IFluentValidatorEntity<TEntity, TValidator>
        where TValidator : AbstractValidator<TEntity>
    {
        IValidator<TEntity> GetValidator();
    }
}
