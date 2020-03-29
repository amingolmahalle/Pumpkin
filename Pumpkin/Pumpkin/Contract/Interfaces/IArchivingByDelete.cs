using System;

namespace CoreService.Framework.Contracts.Domain
{
    public interface IArchivingByDelete
    {
        bool Archived { get; set; }
        
        DateTime? ArchiveTime { get; set; }
        
        string ArchiveUser { get; set; }
    }
}