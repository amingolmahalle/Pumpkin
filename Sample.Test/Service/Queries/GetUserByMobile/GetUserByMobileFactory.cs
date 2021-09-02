using Sample.Test.Domain.Entity.UserAggregate;
using Sample.Test.Domain.Service.Queries.GetUserByMobile;

namespace Sample.Test.Service.Queries.GetUserByMobile
{
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
}