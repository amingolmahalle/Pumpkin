using System;
using Pumpkin.Contract.Domain;
using Pumpkin.Contract.Listeners;
using Pumpkin.Contract.Security;

namespace Pumpkin.Data.Listeners
{
    internal class HistoryBeforeUpdate : IBeforeUpdateListener
    {
        public ICurrentRequest CurrentRequest { get; set; }

        public void OnBeforeUpdate(ChangedEntity entity)
        {
            if (entity.Entity is IHasChangeHistory changeHistory)
            {
                var history = changeHistory;
                
                history.LastUpdateTime = DateTime.UtcNow;
                history.LastUpdateUser = CurrentRequest.UserId;
            }
        }
    }
}