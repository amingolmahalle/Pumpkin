using System;

namespace Pumpkin.Contract.Interfaces
{
    public interface IArchivingByDelete
    {
        bool Archived { get; set; }
        
        DateTime? ArchiveTime { get; set; }
        
        string ArchiveUser { get; set; }
    }
}