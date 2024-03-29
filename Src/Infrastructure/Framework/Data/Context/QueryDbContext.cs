using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Pumpkin.Infrastructure.Framework.Data.Context;

public class QueryDbContext : DbContextBase
{
    public QueryDbContext(DbContextOptions options, IHttpContextAccessor accessor)
        : base(options, accessor)
    {
        ConnectionStringName = "SqlReadNode";
    }
}