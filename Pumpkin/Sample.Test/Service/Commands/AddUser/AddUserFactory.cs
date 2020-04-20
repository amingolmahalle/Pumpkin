using Sample.Test.Domain.Entity.UserAggregate;
using Sample.Test.Domain.Service.Commands.AddUser;

namespace Sample.Test.Service.Commands.AddUser
{
    public class AddUserFactory
    {
        public static User MapEntity(AddUserRequest request)
        {
            return new User
            {
                Fullname = request.Fullname,
                NationalCode = request.NationalCode,
                MobileNumber = request.MobileNumber,
                Status = request.Status,
                BirthDate = request.BirthDate,
                Email = request.Email
            };
        }
    }
}