using System;

namespace Pumpkin.Contract.Domain.Auditable
{
    public interface IModifiableEntity : IAuditableEntity
    {
        DateTime ModifiedAt { get; set; }
        long ModifiedBy { get; set; }
    }
}