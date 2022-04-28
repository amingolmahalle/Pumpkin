using Microsoft.AspNetCore.Mvc.Filters;
using Pumpkin.Contract.Transaction;

namespace Pumpkin.Web.Filters.Transaction;

public class TransactionActionFilter : IAsyncActionFilter
{
    private readonly ITransactionService _transactionService;

    public TransactionActionFilter(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var transactionAttr = context.ActionDescriptor.GetMethodInfo().GetTransactionAttribute();

        using var tran = _transactionService.Begin(transactionAttr.CreateOptions());
        var result = await next();

        if (result.Exception == null || result.ExceptionHandled)
        {
            tran.Complete();
        }
        else
            tran.Rollback();
    }
}