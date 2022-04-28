namespace Pumpkin.Contract.Domain.Auditable;

public interface IRemovableEntity : IAuditableEntity
{
    bool Deleted { get; set; }
        
    DateTime? RemovedAt { get; set; }

    long? RemovedBy { get; set; }
}