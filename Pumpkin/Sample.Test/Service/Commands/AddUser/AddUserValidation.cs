using Sample.Test.Data.Repositories;
using Sample.Test.Domain.Service.Commands.AddUser;

namespace Sample.Test.Service.Commands.AddUser
{
    public class AddUserValidation
    {
        public static void CheckValidation(AddUserRequest request, IUserRepository userRepository)
        {
            // if (string.IsNullOrWhiteSpace(request.PhoneNumber))
            //     throw new UserPhoneNumberNotProvidedException();
            //
            // if (string.IsNullOrWhiteSpace(request.FirstName))
            //     throw new UserFirstNameNotProvidedException();
            //
            // if (string.IsNullOrWhiteSpace(request.LastName))
            //     throw new UserLastNameNotProvidedException();
            //
            // if (userRepository.ExistByPhoneNumberAsync(request.PhoneNumber).Result)
            //     throw new UserPhoneNumberIsAlreadyTakenException();
        }
    }
}