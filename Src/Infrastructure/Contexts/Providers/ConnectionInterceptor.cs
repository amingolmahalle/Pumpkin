using System.Data.Common;
using Domain.Framework.Helpers;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Pumpkin.Infrastructure.Contexts.Providers;

public class ConnectionInterceptor : DbConnectionInterceptor
{
    private readonly string _connectionString;
    public ConnectionInterceptor(string connectionStringKey)
    {
        _connectionString = GlobalConfig.Config[connectionStringKey];
    }
    
    public override InterceptionResult ConnectionOpening(DbConnection connection, ConnectionEventData eventData, InterceptionResult result)
    {
        connection.ConnectionString = _connectionString;
        return result;
    }

    public override ValueTask<InterceptionResult> ConnectionOpeningAsync(DbConnection connection, ConnectionEventData eventData, InterceptionResult result,
        CancellationToken cancellationToken = default)
    {
        connection.ConnectionString = _connectionString;
        return ValueTask.FromResult(result);
    }
}