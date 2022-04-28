using SampleWebApi.Domain.Service.Queries.GetUserByMobile;
using SampleWebApi.Domain.Entity.UserAggregate;

namespace SampleWebApi.Service.Queries.GetUserByMobile;

public class GetUserByMobileFactory
{
    public static GetUserByMobileResponse MapResponse(User user)
    {
        return new GetUserByMobileResponse
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