namespace Domain.Framework.Entities.Auditable;

public interface IRemovableEntity
{
    DateTime? RemovedAt { get; set; }

    Guid? RemovedBy { get; set; }
}