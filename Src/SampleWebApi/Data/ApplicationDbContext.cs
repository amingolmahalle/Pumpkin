using System;
using Microsoft.EntityFrameworkCore;
using Pumpkin.Data;

namespace SampleWebApi.Data
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