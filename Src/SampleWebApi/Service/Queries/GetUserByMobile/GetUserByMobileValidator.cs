using FluentValidation;
using Pumpkin.Common;
using SampleWebApi.Domain.Service.Queries.GetUserByMobile;
using SampleWebApi.Helper;

namespace SampleWebApi.Service.Queries.GetUserByMobile;

public class GetUserByMobileValidator : AbstractValidator<GetUserByMobileRequest>
{
    public GetUserByMobileValidator()
    {
        RuleFor(x => x.MobileNumber).NotNull()
            .Matches(Constants.MobileNumberPattern)
            .WithMessage(Messages.MobileNumberRequired);
    }
}