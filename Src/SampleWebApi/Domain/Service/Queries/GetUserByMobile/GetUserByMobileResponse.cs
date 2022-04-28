using System;

namespace SampleWebApi.Domain.Service.Queries.GetUserByMobile
{
    public class GetUserByMobileResponse
    {
        public int Id { get; set; }

        public string Fullname { get; set; }

        public string MobileNumber { get; set; }

        public string NationalCode { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Status { get; set; }

        public DateTime SubmitTime { get; set; }
    }
}