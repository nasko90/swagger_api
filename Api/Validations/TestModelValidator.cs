using System;
using Data_Access.Models;
using FluentValidation;

namespace Api.Validations
{
    public class TestModelValidator : AbstractValidator<TestModel>
    {
        private readonly IServiceProvider _serviceProvider;

        public TestModelValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Title).MaximumLength(100);
            RuleFor(x => x.Duration).NotEmpty();
            RuleFor(x => x.Duration).GreaterThan(0);
            RuleFor(x => x.Duration).LessThan(1000);
        }
    }
}