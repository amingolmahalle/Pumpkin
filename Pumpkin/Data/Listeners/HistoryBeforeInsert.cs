using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pumpkin.Contract.Domain.Auditable;
using Pumpkin.Contract.Listeners;
using Pumpkin.Contract.Security;

namespace Pumpkin.Data.Listeners
{
    internal class HistoryBeforeInsert : IBeforeInsertListener
    {
        private readonly ICurrentRequest _currentRequest;

        public HistoryBeforeInsert(ICurrentRequest currentRequest)
        {
            _currentRequest = currentRequest;
        }

        public void OnBeforeInsert(EntityEntry entry)
        {
            if (entry.Entity is ICreatableEntity creatableEntity)
            {
                ICreatableEntity history = creatableEntity;

                history.CreatedAt = DateTime.UtcNow;
                history.CreatedBy = _currentRequest.UserId;
            }
        }
    }
}