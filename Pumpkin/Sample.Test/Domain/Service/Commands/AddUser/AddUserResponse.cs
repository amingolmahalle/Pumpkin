using System;

namespace Sample.Test.Domain.Service.Commands.AddUser
{
    public class AddUserResponse
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset? CreatedDateTime { get; set; }

        public DateTimeOffset? LastVisitDateTime { get; set; }

        public string PhoneNumber { get; set; }

        public bool Status { get; set; }
    }
}