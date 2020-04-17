using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pumpkin.Contract.Domain;
using Pumpkin.Contract.Listeners;
using Pumpkin.Contract.Security;

namespace Pumpkin.Data.Listeners
{
    public class HistoryBeforeDelete : IBeforeDeleteListener
    {
        public ICurrentRequest CurrentRequest { get; set; }

        public void OnBeforeDelete(EntityEntry entry)
        {
            if (entry.Entity is ISoftDelete changeHistory)
            {
                entry.State = EntityState.Modified;
                
                var history = changeHistory;

                history.ArchiveTime = DateTime.UtcNow;
                history.ArchiveUser = CurrentRequest.UserId;
                history.Deleted = true;
            }
        }
    }
}