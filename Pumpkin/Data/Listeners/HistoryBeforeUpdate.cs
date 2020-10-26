using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pumpkin.Contract.Domain;
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
            if (entry.Entity is IHasChangeHistory changeHistory)
            {
                var history = changeHistory;

                history.LastUpdateTime = DateTime.UtcNow;
                history.LastUpdateUser = _currentRequest.UserId;
            }
        }
    }
}