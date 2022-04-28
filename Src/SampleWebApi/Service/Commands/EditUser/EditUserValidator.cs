using FluentValidation;
using Pumpkin.Common;
using SampleWebApi.Domain.Service.Commands.EditUser;
using SampleWebApi.Helper;

namespace SampleWebApi.Service.Commands.EditUser;

public class EditUserValidator: AbstractValidator<EditUserRequest>
{
    public EditUserValidator()
    {
        RuleFor(x => x.Id).NotNull()
            .WithMessage(Messages.UserIdRequired);
            
        When(x => x != null, () =>
        {
            RuleFor(x => x.FullName).NotEmpty()
                .WithMessage(Messages.FullNameRequired);
        });
            
        When(x => x != null, () =>
        {
            RuleFor(x => x.Email).NotEmpty()
                .Matches(Constants.EmailPattern)
                .WithMessage(Messages.EmailRequired);
        });
            
        When(x => x != null, () =>
        {
            RuleFor(x => x.MobileNumber).NotEmpty()
                .Matches(Constants.MobileNumberPattern)
                .WithMessage(Messages.MobileNumberRequired);
        });
            
        When(x => x != null, () =>
        {
            RuleFor(x => x.NationalCode).NotEmpty()
                .Matches(Constants.NationalCodePattern)
                .WithMessage(Messages.NationalCodeRequired);
        });
    }
}