using System;
using System.Threading.Tasks;

namespace Pumpkin.Contract.Domain
{
    public interface IUnitOfWork
    {
        Task Begin();
        
        Task End(Exception ex = null);
    }
}
