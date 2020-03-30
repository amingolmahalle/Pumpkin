using System;

namespace Pumpkin.Contract.Domain
{
    public interface IArchivingByDelete
    {
        bool Archived { get; set; }
        
        DateTime? ArchiveTime { get; set; }
        
        string ArchiveUser { get; set; }
    }
}