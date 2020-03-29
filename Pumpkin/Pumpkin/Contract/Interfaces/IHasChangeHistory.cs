using System;

namespace CoreService.Framework.Contracts.Domain
{
    public interface IHasChangeHistory
    {
        DateTime SubmitTime { get; set; }
        
        DateTime? LastUpdateTime { get; set; }
        
        string SubmitUser { get; set; }
        
        string LastUpdateUser { get; set; }
    }
}