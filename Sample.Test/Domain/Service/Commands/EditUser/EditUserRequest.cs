using System;

namespace Sample.Test.Domain.Service.Commands.EditUser
{
    public class EditUserRequest
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string MobileNumber { get; set; }

        public string NationalCode { get; set; }

        public string Email { get; set; }
        
        public DateTime? BirthDate { get; set; }
    }
}