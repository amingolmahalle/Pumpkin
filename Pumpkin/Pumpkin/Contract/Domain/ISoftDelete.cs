using System;

namespace Pumpkin.Contract.Domain
{
    public interface ISoftDelete
    {
        bool Archived { get; set; }
        
        DateTime? ArchiveTime { get; set; }
        
        int ArchiveUser { get; set; }
    }
}