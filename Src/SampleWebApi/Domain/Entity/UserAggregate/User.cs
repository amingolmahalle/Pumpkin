using Pumpkin.Contract.Domain;

namespace SampleWebApi.Domain.Entity.UserAggregate;

public class User : IntAuditableEntity, IAggregateRoot 
{
    public string Fullname { get; set; }

    public string MobileNumber { get; set; }

    public string NationalCode { get; set; }

    public string Email { get; set; }

    public DateTime BirthDate { get; set; }

    public bool Status { get; set; }
}