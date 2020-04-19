using System;

namespace Pumpkin.Contract.Domain
{
    public interface IHasChangeHistory
    {
        DateTime SubmitTime { get; set; }
        
        DateTime? LastUpdateTime { get; set; }
        
        int? SubmitUser { get; set; }
        
        int? LastUpdateUser { get; set; }
    }
}