using System;
using Pumpkin.Contract.Domain;
using Pumpkin.Contract.Listeners;
using Pumpkin.Contract.Security;

namespace Pumpkin.Data.Listeners
{
    public class HistoryBeforeDelete : IBeforeDeleteListener
    {
        public ICurrentRequest CurrentRequest { get; set; }

        public void OnBeforeDelete(object entity)
        {
            if (entity is ISoftDelete changeHistory)
            {
                var history = changeHistory;

                history.ArchiveTime = DateTime.UtcNow;
                history.ArchiveUser = CurrentRequest.UserId;
                history.Archived = true;
            }
        }
    }
}