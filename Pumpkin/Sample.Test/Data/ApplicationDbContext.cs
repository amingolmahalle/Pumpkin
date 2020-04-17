using Microsoft.EntityFrameworkCore;
using Pumpkin.Contract.Listeners;
using Pumpkin.Data;

namespace Sample.Test.Data
{
    public class ApplicationDbContext : DatabaseContext
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IBeforeInsertListener beforeInsertListener,
            IBeforeUpdateListener beforeUpdateListener,
            IBeforeDeleteListener beforeDeleteListener)
            : base(
                options,
                beforeInsertListener,
                beforeUpdateListener,
                beforeDeleteListener)
        {
        }
    }
}