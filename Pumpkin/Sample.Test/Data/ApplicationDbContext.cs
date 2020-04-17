using System;
using Microsoft.EntityFrameworkCore;
using Pumpkin.Data;

namespace Sample.Test.Data
{
    public class ApplicationDbContext : DatabaseContext
    {
        public ApplicationDbContext(DbContextOptions options, IServiceProvider serviceProvider)
            : base(options, serviceProvider)
        {
        }
    }
}