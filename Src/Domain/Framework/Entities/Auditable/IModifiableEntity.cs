namespace Domain.Framework.Entities.Auditable;

public interface IModifiableEntity
{
    DateTime? ModifiedAt { get; set; }
    Guid? ModifiedBy { get; set; }
}