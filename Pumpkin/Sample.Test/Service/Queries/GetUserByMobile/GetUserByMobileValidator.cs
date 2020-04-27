using FluentValidation;
using Pumpkin.Utils;
using Sample.Test.Domain.Service.Queries.GetUserByMobile;
using Sample.Test.Helper;

namespace Sample.Test.Service.Queries.GetUserByMobile
{
    public class GetUserByMobileValidator : AbstractValidator<GetUserByMobileRequest>
    {
        public GetUserByMobileValidator()
        {
            RuleFor(x => x.MobileNumber).NotNull()
                .Matches(Constants.MobileNumberPattern)
                .WithMessage(Messages.MobileNumberRequired);
        }
    }
}