using FluentValidation;
using Sample.Test.Domain.Service.Queries.GetUserById;
using Sample.Test.Helper;

namespace Sample.Test.Service.Queries.GetUserById
{
    public class GetUserByIdValidator : AbstractValidator<GetUserByIdRequest>
    {
        public GetUserByIdValidator()
        {
            RuleFor(x => x.Id).NotNull()
                .WithMessage(Messages.UserIdRequired);
        }
    }
}