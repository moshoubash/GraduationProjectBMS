using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectBMS.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ICategoryManager categoryManager;
        public HomeController(ICategoryManager categoryManager)
        {
            this.categoryManager = categoryManager;
        }
        public async Task<IActionResult> Category(int id)
        {
            var category = await categoryManager.GetCategoryWithArticlesAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var viewModel = new CategoryViewModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Articles = category.Articles
            };

            return View(viewModel);
        }
    }
}
