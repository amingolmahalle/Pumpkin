using System;

namespace Sample.Test.Domain.Service.Commands.AddUser
{
    public class AddUserRequest
    {
        public string Fullname { get; set; }

        public string MobileNumber { get; set; }

        public string NationalCode { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }
    }
}