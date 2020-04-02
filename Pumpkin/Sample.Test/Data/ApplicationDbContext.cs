using Microsoft.EntityFrameworkCore;
using Pumpkin.Data;

namespace Sample.Test.Data
{
    public class ApplicationDbContext : DatabaseContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}