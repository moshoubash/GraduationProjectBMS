using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GraduationProjectBMS.Pages
{
    [Authorize]
    public class CreateArticleModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
