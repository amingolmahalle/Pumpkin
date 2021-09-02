using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pumpkin.Contract.Domain.Auditable;
using Pumpkin.Contract.Listeners;
using Pumpkin.Contract.Security;

namespace Pumpkin.Data.Listeners
{
    public class HistoryBeforeDelete : IBeforeDeleteListener
    {
        private readonly ICurrentRequest _currentRequest;

        public HistoryBeforeDelete(ICurrentRequest currentRequest)
        {
            _currentRequest = currentRequest;
        }

        public void OnBeforeDelete(EntityEntry entry)
        {
            if (entry.Entity is IRemovableEntity removableEntity)
            {
                entry.State = EntityState.Modified;

                var history = removableEntity;

                history.RemovedAt = DateTime.UtcNow;
                history.RemovedBy = _currentRequest.UserId;
                history.Deleted = true;
            }
        }
    }
}