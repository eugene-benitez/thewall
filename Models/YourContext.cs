using Microsoft.EntityFrameworkCore;


//CHANGE THIS
namespace The_Wall.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        // "users" table is represented by this DbSet "Users"
        public DbSet<Users> Users { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Comments> Comments { get; set; }

    }
}
