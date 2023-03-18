using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Pumpkin.Infrastructure.Framework.Data.Context;

public class CommandDbContext : DbContextBase
{
    public CommandDbContext(DbContextOptions options, IHttpContextAccessor accessor)
        : base(options, accessor)
    {
        ConnectionStringName = "SqlWriteNode";
    }
}