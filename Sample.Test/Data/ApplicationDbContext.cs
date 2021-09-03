using System;
using Microsoft.EntityFrameworkCore;
using Pumpkin.Data;

namespace Sample.Test.Data
{
    public class ApplicationDbContext : DatabaseContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IServiceProvider serviceProvider)
            : base(options, serviceProvider)
        {
        }
    }
}