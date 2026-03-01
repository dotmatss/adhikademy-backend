using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class AdhikademyDbContext : DbContext
    {
        public AdhikademyDbContext(DbContextOptions<AdhikademyDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
    }
}
