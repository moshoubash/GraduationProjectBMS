using GraduationProjectBMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace GraduationProjectBMS.Services
{
    public class MyDbContext : IdentityDbContext<AppUser>
    {
        public MyDbContext(DbContextOptions<MyDbContext> dbContext) : base(dbContext)
        {
            
        }

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder builder) {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";
            var user = new IdentityRole("user");
            user.NormalizedName = "user";

            builder.Entity<IdentityRole>().HasData(admin, user);

            // one to many

            builder.Entity<Article>()
                .HasOne(a => a.AppUser)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.Id);

            // one to many

            builder.Entity<Article>()
                .HasOne(a => a.Category)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.CategoryId);

            // one to many
            builder.Entity<Category>()
            .HasMany(c => c.Articles)
            .WithOne(a => a.Category)
            .HasForeignKey(a => a.CategoryId);

            // one-to-many

            builder.Entity<Comment>()
            .HasOne(c => c.Article)
            .WithMany(a => a.Comments)
            .HasForeignKey(c => c.ArticleId);

            builder.Entity<Comment>()
                .HasOne(c => c.AppUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId);

            // one-to-many

            builder.Entity<Reply>()
                .HasOne(a => a.AppUser)
                .WithMany(u => u.Replies)
                .HasForeignKey(a => a.UserId);

            // one-to-many

            builder.Entity<Like>()
                .HasOne(a => a.AppUser)
                .WithMany(u => u.Likes)
                .HasForeignKey(a => a.UserId);

            builder.Entity<Like>()
                .HasOne(a => a.Article)
                .WithMany(x => x.Likes)
                .HasForeignKey(a => a.ArticleId);

            // one-to-many

            builder.Entity<Tag>()
                .HasOne(a => a.Article)
                .WithMany(u => u.Tags)
                .HasForeignKey(a => a.ArticleId);
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Like> Likes { get; set; }
    }
}
