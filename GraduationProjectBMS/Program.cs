using GraduationProjectBMS.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GraduationProjectBMS.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using GraduationProjectBMS.Email;
using GraduationProjectBMS.Repositories.User;
using GraduationProjectBMS.Repositories.Category;

namespace GraduationProjectBMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<MyDbContext>(op => op.UseSqlServer(connectionString));
            builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MyDbContext>();
            builder.Services.AddMvc(op => op.EnableEndpointRouting = false);
            builder.Services.AddSignalR();
            
            
            // repository services injection
            
            // register badwords service
            builder.Services.AddSingleton<BadWordsFilterService>();
            
            // register email sender service
            builder.Services.AddSingleton<IEmailSender, EmailSender>();
            
            // register Article services
            builder.Services.AddTransient<IArticleManager, ArticleManager>();
            
            // register Users services
            builder.Services.AddTransient<IUserFunctions, UserFunctions>();
            
            // register Categories services
            builder.Services.AddTransient<ICategoryManager, CategoryManager>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseMvcWithDefaultRoute();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapHub<ChatHub>("/chatHub");
            app.Run();
        }
    }
}
