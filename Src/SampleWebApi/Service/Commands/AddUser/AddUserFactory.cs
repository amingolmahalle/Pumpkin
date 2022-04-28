using SampleWebApi.Domain.Service.Commands.AddUser;
using SampleWebApi.Domain.Entity.UserAggregate;

namespace SampleWebApi.Service.Commands.AddUser;

public class AddUserFactory
{
    public static User MapEntity(AddUserRequest request)
    {
        return new User
        {
            Fullname = request.Fullname,
            NationalCode = request.NationalCode,
            MobileNumber = request.MobileNumber,
            Status = true,
            BirthDate = request.BirthDate,
            Email = request.Email
        };
    }
}