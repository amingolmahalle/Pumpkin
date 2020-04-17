using System;
using Pumpkin.Contract.Domain;
using Pumpkin.Contract.Listeners;
using Pumpkin.Contract.Security;

namespace Pumpkin.Data.Listeners
{
    internal class HistoryBeforeInsert : IBeforeInsertListener
    {
        public ICurrentRequest CurrentRequest { get; set; }

        public void OnBeforeInsert(object entity)
        {
            if (entity is IHasChangeHistory changeHistory)
            {
                var history = changeHistory;

                history.SubmitTime = DateTime.UtcNow;
                history.SubmitUser = CurrentRequest.UserId;
            }
        }
    }
}