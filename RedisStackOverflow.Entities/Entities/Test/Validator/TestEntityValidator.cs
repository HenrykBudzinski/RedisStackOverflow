using FluentValidation;

namespace RedisStackOverflow.Entities.Test.Validator
{

    public class TestEntityValidator : AbstractValidator<TestEntity>
    {
    }

    public class DependentTestEntityValidator : AbstractValidator<DependentTestEntity>
    {
    }
}
