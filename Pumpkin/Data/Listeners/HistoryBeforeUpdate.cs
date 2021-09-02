using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pumpkin.Contract.Domain.Auditable;
using Pumpkin.Contract.Listeners;
using Pumpkin.Contract.Security;

namespace Pumpkin.Data.Listeners
{
    internal class HistoryBeforeUpdate : IBeforeUpdateListener
    {
        private readonly ICurrentRequest _currentRequest;

        public HistoryBeforeUpdate(ICurrentRequest currentRequest)
        {
            _currentRequest = currentRequest;
        }

        public void OnBeforeUpdate(EntityEntry entry)
        {
            if (entry.Entity is IModifiableEntity modifiableEntity)
            {
                IModifiableEntity history = modifiableEntity;

                history.ModifiedAt = DateTime.UtcNow;
                history.ModifiedBy = _currentRequest.UserId;
            }
        }
    }
}