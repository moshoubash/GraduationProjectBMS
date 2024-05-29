using GraduationProjectBMS.Models;
using GraduationProjectBMS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectBMS.Repositories.User
{
    public class UserFunctions : IUserFunctions
    {
        private readonly MyDbContext dbContext;
        private readonly UserManager<AppUser> userManager;
        public UserFunctions(MyDbContext dbContext, UserManager<AppUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        void IUserFunctions.DeleteUser(string id)
        {
            var targetUser = (from x in dbContext.Users where x.Id == id select x).FirstOrDefault();
            dbContext.Users.Remove(targetUser);
            dbContext.SaveChanges();
        }

        AppUser IUserFunctions.GetUser(string id)
        {
            var targetUser = (from x in dbContext.Users where x.Id == id select x).FirstOrDefault();
            return targetUser;
        }

        List<AppUser> IUserFunctions.GetUsers()
        {
            return dbContext.Users.ToList();
        }
    }
}
