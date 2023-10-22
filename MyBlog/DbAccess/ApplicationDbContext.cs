using MyBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.DbAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<AuthUser> AuthUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //為每個Tabel詳細定義內容
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
