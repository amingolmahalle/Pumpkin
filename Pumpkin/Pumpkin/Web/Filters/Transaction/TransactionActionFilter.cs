using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Pumpkin.Contract.Transaction;

namespace Pumpkin.Web.Filters.Transaction
{
    public class TransactionActionFilter : IAsyncActionFilter
    {
        public ITransactionService TransactionService { get; set; }

        public TransactionActionFilter(ITransactionService transactionService)
        {
            TransactionService = transactionService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var transactionAttr = context.ActionDescriptor.GetMethodInfo().GetTransactionAttribute();

            using (var tran = TransactionService.Begin(transactionAttr.CreateOptions()))
            {
                var result = await next();
                
                if (result.Exception == null || result.ExceptionHandled)
                {
                    tran.Complete();
                }
                else
                    tran.Rollback();
            }
        }
    }
}