using System;

namespace Pumpkin.Contract.Domain
{
    public interface ISoftDelete
    {
        bool Deleted { get; set; }
        
        DateTime? ArchiveTime { get; set; }
        
        int ArchiveUser { get; set; }
    }
}