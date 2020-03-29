using System;
using System.Threading.Tasks;

namespace Pumpkin.Contract.Interfaces
{
    public interface IUnitOfWork
    {
        Task Begin();
        
        Task End(Exception ex = null);
    }
}
