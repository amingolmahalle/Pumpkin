using FluentValidation;
using Pumpkin.Common;
using Sample.Test.Domain.Service.Commands.AddUser;
using Sample.Test.Helper;

namespace Sample.Test.Service.Commands.AddUser
{
    public class AddUserValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserValidator()
        {
            RuleFor(x => x.Fullname).NotNull()
                .WithMessage(Messages.FullNameRequired);

            RuleFor(x => x.MobileNumber).NotNull()
                .Matches(Constants.MobileNumberPattern)
                .WithMessage(Messages.MobileNumberRequired);

            RuleFor(x => x.NationalCode).NotNull()
                .Matches(Constants.NationalCodePattern)
                .WithMessage(Messages.NationalCodeRequired);

            When(x => x != null, () =>
            {
                RuleFor(x => x.Email).NotEmpty()
                    .Matches(Constants.EmailPattern)
                    .WithMessage(Messages.EmailRequired);
            });
        }
    }
}