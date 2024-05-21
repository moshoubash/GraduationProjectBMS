using GraduationProjectBMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GraduationProjectBMS.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleManager articleManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signManager;
        public ArticleController(UserManager<AppUser> userManager, IArticleManager articleManager, IWebHostEnvironment webHostEnvironment, SignInManager<AppUser> signManager)
        {
            this.articleManager = articleManager;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.signManager = signManager;
        }
        // GET: ArticleController
        public async Task<ActionResult> Index()
        {
            if (signManager.IsSignedIn(User))
            {
                var user = await userManager.GetUserAsync(User);
                return View(articleManager.GetUserArticles(user.Id));
            }
            else {
                return View();
            }
        }

        // GET: ArticleController/Details/5
        public ActionResult Details(int id)
        {
            return View(articleManager.GetArticle(id));
        }

        // GET: ArticleController/Create
        [Authorize(Roles = "user")]
        public ActionResult Create()
        {
            return View();
        }
        // POST: ArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user")]
        public async Task<ActionResult> Create(Article article, IFormFile ArticleThumbnail)
        {
            try
            {
                if (ArticleThumbnail != null) {
                    var wwroot = webHostEnvironment.WebRootPath + "/ArticlesThumbnails";
                    Guid guid = Guid.NewGuid();
                    var fullPath = System.IO.Path.Combine(wwroot, guid + ArticleThumbnail.FileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        ArticleThumbnail.CopyTo(stream);
                    }

                    article.ArticleThumbnail = guid + ArticleThumbnail.FileName;
                }
                article.CreatedAt = DateTime.Now;
                article.EditAt = DateTime.Now;
                var user = await userManager.GetUserAsync(User);
                article.Id = user.Id;
                article.CategoryId = 1;
                articleManager.CreateArticle(article);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArticleController/Edit/5
        [Authorize(Roles = "user")]
        public ActionResult Edit(int id)
        {
            return View(articleManager.GetArticle(id));
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Article article)
        {
            try
            {
                article.EditAt = DateTime.Now;
                articleManager.EditArticle(id, article);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                articleManager.DeleteArticle(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
