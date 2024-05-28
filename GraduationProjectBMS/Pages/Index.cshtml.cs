using Microsoft.AspNetCore.Mvc;
using GraduationProjectBMS.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GraduationProjectBMS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectBMS
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly MyDbContext dbContext;
        public IndexModel(ILogger<IndexModel> logger, MyDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        public void OnGet()
        {

        }

        public List<Article> GetArticles() {
            return dbContext.Articles.ToList();
        }
    }
}
