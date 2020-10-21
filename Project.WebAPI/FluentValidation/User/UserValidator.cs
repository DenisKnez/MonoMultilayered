using FluentValidation;
using System;

namespace Project.WebAPI.FluentValidation.User
{
    public class UserValidator : AbstractValidator<UserRestModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).MinimumLength(4);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.DateOfBirth).ExclusiveBetween(new DateTime(1920, 1, 1), new DateTime(2010, 1, 1));
        }
    }
}