using FluentValidation;
using SampleWebApi.Domain.Service.Queries.GetUserById;
using SampleWebApi.Helper;

namespace SampleWebApi.Service.Queries.GetUserById;

public class GetUserByIdValidator : AbstractValidator<GetUserByIdRequest>
{
    public GetUserByIdValidator()
    {
        RuleFor(x => x.Id).NotNull()
            .WithMessage(Messages.UserIdRequired);
    }
}