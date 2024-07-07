using Yad2.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace Yad2.Data
{
    public class Yad2DbContext : DbContext
    {
        public Yad2DbContext(DbContextOptions<Yad2DbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Subcategory> Subcategory { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Favorite> Favorite { get; set; }
        public virtual DbSet<LoginHistory> LoginHistory { get; set; }
        public virtual DbSet<Reporting> Reporting { get; set; }

            
    }
}
