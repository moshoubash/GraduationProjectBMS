using GraduationProjectBMS.Models;
using GraduationProjectBMS.Repositories.User;
using GraduationProjectBMS.Services;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography.Pkcs;
using System.Text.RegularExpressions;

namespace GraduationProjectBMS.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleManager articleManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signManager;
        private readonly MyDbContext dbContext;
        public ArticleController(UserManager<AppUser> userManager, IArticleManager articleManager, IWebHostEnvironment webHostEnvironment, SignInManager<AppUser> signManager,  MyDbContext dbContext)
        {
            this.articleManager = articleManager;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.signManager = signManager;
            this.dbContext = dbContext;
        }
        // GET: ArticleController
        [Authorize(Roles = "user")]
        public async Task<ActionResult> Index()
        {
            if (signManager.IsSignedIn(User))
            {
                var user = await userManager.GetUserAsync(User);
                if (user != null)
                    return View(articleManager.GetUserArticles(user.Id));
                else
                    return View();
            }
            else
            {
                return View();
            }
        }

        // GET: ArticleController/Details/5
        public ActionResult Details(int id)
        {
            var article = dbContext.Articles
            .Include(a => a.Likes)
            .FirstOrDefault(a => a.ArticleId == id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: ArticleController/Create
        [Authorize(Roles = "user")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(dbContext.Categories, "CategoryId", "CategoryName");
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
                if (ModelState.IsValid) {
                    if (ArticleThumbnail != null)
                    {
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
                    article.UserFullName = user.FullName;
                    articleManager.CreateArticle(article);
                    return RedirectToAction(nameof(Index));
                }
                var categories = dbContext.Categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToList();

                ViewBag.CategoryId = categories;

                return View(article);
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
        public async Task<ActionResult> Edit(int id, Article article, IFormFile ArticleThumbnail)
        {
            try
            {
                article.EditAt = DateTime.Now;
                if (ArticleThumbnail != null)
                {
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
                article.UserFullName = user.FullName;
                article.CategoryId = 1;
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

        [HttpGet]
        public IActionResult Search(string query) {
            return View(articleManager.GetSearchArticles(query));
        }

        public async Task<ActionResult> ToggleLike(int ArticleId)
        {
            // Get the current user
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            // check article exists
            var targetArticle = await dbContext.Articles.FirstOrDefaultAsync(x => x.ArticleId == ArticleId);
            if (targetArticle == null)
            {
                return NotFound();
            }

            // Check
            var existingLike = await dbContext.Likes.FirstOrDefaultAsync(l => l.ArticleId == ArticleId && l.UserId == user.Id);

            if (existingLike != null)
            {
                // already liked
                dbContext.Likes.Remove(existingLike);
                await dbContext.SaveChangesAsync();
                return Redirect($"/Article/Details/{ArticleId}");
            }
            else
            {
                // User has not liked the article, so we add a new like
                var like = new Like
                {
                    ArticleId = ArticleId,
                    UserId = user.Id
                };
                dbContext.Likes.Add(like);
                await dbContext.SaveChangesAsync();
                return Redirect($"/Article/Details/{ArticleId}");
            }
        }
    }
}