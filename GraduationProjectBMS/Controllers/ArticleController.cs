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
        public ArticleController(UserManager<AppUser> userManager, IArticleManager articleManager, IWebHostEnvironment webHostEnvironment)
        {
            this.articleManager = articleManager;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
        }
        // GET: ArticleController
        public ActionResult Index()
        {
            return View(articleManager.GetArticles());
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
        [Authorize(Roles = "user")]
        // POST: ArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article, IFormFile ArticleThumbnail)
        {
            try
            {
                var wwroot = webHostEnvironment.WebRootPath + "/ArticlesThumbnails";
                Guid guid = Guid.NewGuid();
                var fullPath = System.IO.Path.Combine(wwroot, guid+ ArticleThumbnail.FileName);

                using (var stream = new FileStream(fullPath, FileMode.Create)) {
                    ArticleThumbnail.CopyTo(stream);
                }

                article.ArticleThumbnail = guid + ArticleThumbnail.FileName;
                article.CreatedAt = DateTime.Now;
                article.EditAt = DateTime.Now;
                /*var userId = await userManager.GetUserIdAsync();*//**/
                /*article.Id = userId;*/

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
