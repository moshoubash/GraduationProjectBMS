using GraduationProjectBMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GraduationProjectBMS.Pages
{
    [Authorize]
    public class CreateArticleModel : PageModel
    {
        protected readonly UserManager<AppUser> userManager;
        public AppUser? appUser;
        public CreateArticleModel(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public void OnGet()
        {
            var task = userManager.GetUserAsync(User);
            task.Wait();
            appUser = task.Result;
        }
    }
}
