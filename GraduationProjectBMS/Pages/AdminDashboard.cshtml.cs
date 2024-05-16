using GraduationProjectBMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectBMS.Pages
{
    [Authorize(Roles = "admin")]
    public class AdminDashboardModel : PageModel {
        public void OnGet()
        {
        }
    }
}
