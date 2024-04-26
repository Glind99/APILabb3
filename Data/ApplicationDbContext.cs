using APILabb3.Models;
using APILabb3.NewFolder;
using Microsoft.EntityFrameworkCore;

namespace APILabb3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Interest> Interests { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Person> Persons { get; set; }

    }
}
