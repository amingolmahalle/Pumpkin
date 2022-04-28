using SampleWebApi.Domain.Service.Commands.EditUser;
using SampleWebApi.Domain.Entity.UserAggregate;

namespace SampleWebApi.Service.Commands.EditUser;

public class EditUserFactory
{
    public static void MapEntity(ref User user, EditUserRequest request)
    {
        user.Fullname = request.FullName ?? user.Fullname;
        user.MobileNumber = request.MobileNumber ?? user.MobileNumber;
        user.NationalCode = request.NationalCode ?? user.NationalCode;
        user.Email = request.Email ?? user.Email;
        user.BirthDate = request.BirthDate ?? user.BirthDate;
    }
}