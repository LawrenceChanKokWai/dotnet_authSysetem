using APIProject.Models;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Data.Control
{
    public class UserContext : DbContext
    {

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<RoleModel> RoleModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().ToTable("tbl_User");
            modelBuilder.Entity<RoleModel>().ToTable("tbl_Role");
        }
    }
}
