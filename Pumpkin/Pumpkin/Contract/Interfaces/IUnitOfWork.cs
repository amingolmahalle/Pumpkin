using System;
using System.Threading.Tasks;

namespace CoreService.Framework.Contracts.Domain
{
    public interface IUnitOfWork
    {
        Task Begin();
        
        Task End(Exception ex = null);
    }
}
