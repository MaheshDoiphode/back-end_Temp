using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorManagement.Domain.Common;

namespace VisitorManagement.Infrastructure.Data
{
    public class VisitorManagementApplicationContext : DbContext
    {
        public VisitorManagementApplicationContext(DbContextOptions<VisitorManagementApplicationContext> options)
    : base(options)
        {
        }

        public DbSet<Visitor> Visitors { get; set; }


    }
}
