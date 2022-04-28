using SampleWebApi.Domain.Service.Queries.GetUserById;
using SampleWebApi.Domain.Entity.UserAggregate;

namespace SampleWebApi.Service.Queries.GetUserById;

public class GetUserByIdFactory
{
    public static GetUserByIdResponse MapResponse(User user)
    {
        return new GetUserByIdResponse
        {
            Id = user.Id,
            MobileNumber = user.MobileNumber,
            Fullname = user.Fullname,
            NationalCode = user.NationalCode ?? string.Empty,
            Status = user.Status,
            BirthDate = user.BirthDate,
            Email = user.Email,
            SubmitTime = user.CreatedAt,
        };
    }
}