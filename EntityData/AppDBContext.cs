using Microsoft.EntityFrameworkCore;
using YellowPagesAPI.Models;

namespace YellowPagesAPI.EntityData
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
