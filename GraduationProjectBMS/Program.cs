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
            builder.Services.AddSingleton<BadWordsFilterService>();
            // repository services injection
            builder.Services.AddSingleton<IEmailSender, EmailSender>();
            builder.Services.AddTransient<IArticleManager, ArticleManager>();
            builder.Services.AddTransient<IUserFunctions, UserFunctions>();
            builder.Services.AddTransient<ICategoryManager, CategoryManager>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            // Training model
            /*ModelBuilder.Train();
            Console.WriteLine("Model training complete.");
*/
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
