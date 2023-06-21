using Microsoft.EntityFrameworkCore;
using ReactApiTest.Core.Models;

namespace ReactApiTest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
        public DbSet<Person> People { get; set; }
    }
}
