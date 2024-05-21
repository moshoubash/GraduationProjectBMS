using GraduationProjectBMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace GraduationProjectBMS.Services
{
    public class MyDbContext : IdentityDbContext<AppUser>
    {
        public MyDbContext(DbContextOptions<MyDbContext> dbContext) : base(dbContext)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";
            var user = new IdentityRole("user");
            user.NormalizedName = "user";

            builder.Entity<IdentityRole>().HasData(admin, user);

            // Configure the one-to-many relationship - Article

            builder.Entity<Article>()
                .HasOne(a => a.AppUser)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.Id);

            builder.Entity<Article>()
                .HasOne(a => a.Category)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.CategoryId);

            // Configure the one-to-many relationship - Comments

            builder.Entity<Comment>()
                .HasOne(a => a.AppUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(a => a.UserId);

            // Configure the one-to-many relationship - Replies

            builder.Entity<Reply>()
                .HasOne(a => a.AppUser)
                .WithMany(u => u.Replies)
                .HasForeignKey(a => a.UserId);

            // Configure the one-to-many relationship - likes

            builder.Entity<Like>()
                .HasOne(a => a.AppUser)
                .WithMany(u => u.Likes)
                .HasForeignKey(a => a.UserId);

            // Configure the one-to-many relationship - tags

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
