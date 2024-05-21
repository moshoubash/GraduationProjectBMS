using Microsoft.AspNetCore.Mvc;
using GraduationProjectBMS.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GraduationProjectBMS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraduationProjectBMS
{
    public class IndexModel : PageModel
    {
        private readonly MyDbContext _dbContext;
        private readonly ILogger<IndexModel> _logger;
        public readonly UserManager<AppUser> _usermanager;
        public readonly SignInManager<AppUser> _signmanager;
        public IndexModel(ILogger<IndexModel> logger, MyDbContext _dbContext, UserManager<AppUser> _usermanager, SignInManager<AppUser> _signmanager)
        {
            _logger = logger;
            this._dbContext = _dbContext;
            this._signmanager = _signmanager;
        }

        public void OnGet()
        {

        }
/*
        public List<Article> GetArticles() {
            return _dbContext.Articles.ToList();
        }*/
    }
}
