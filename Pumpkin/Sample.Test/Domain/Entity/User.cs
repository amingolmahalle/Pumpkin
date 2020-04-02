using System;
using Pumpkin.Contract.Domain;

namespace Sample.Test.Domain.Entity
{
    public class User : IEntity<User, int>, IHasChangeHistory
    {
        public int Id { get; set; }

        public string Fullname { get; set; }

        public string MobileNumber { get; set; }

        public string NationalCode { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Status { get; set; }

        public DateTime SubmitTime { get; set; }

        public DateTime? LastUpdateTime { get; set; }

        public int SubmitUser { get; set; }

        public int? LastUpdateUser { get; set; }
    }
}