using GraduationProjectBMS.Models;
using GraduationProjectBMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectBMS.Controllers
{
    public class CategoryController : Controller
    {
        private readonly MyDbContext _context;
        private readonly ICategoryManager categoryManager;
        public CategoryController(MyDbContext _context, ICategoryManager categoryManager)
        {
            this._context = _context;
            this.categoryManager = categoryManager;

        }
        // GET: CategoryController
        public ActionResult List()
        {
            return View(categoryManager.GetCategories());
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create(Category category)
        {
            try
            {
                categoryManager.CreateCategory(category);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View(category);
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                categoryManager.DeleteCategory(id);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryWithArticles(int id)
        {
            var category = await categoryManager.GetCategoryWithArticlesAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
    }
}
