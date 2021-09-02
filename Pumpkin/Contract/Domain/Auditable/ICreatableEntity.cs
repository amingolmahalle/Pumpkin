using System;

namespace Pumpkin.Contract.Domain.Auditable
{
    public interface ICreatableEntity : IAuditableEntity
    {
        DateTime CreatedAt { get; set; }
        long CreatedBy { get; set; }
    }
}