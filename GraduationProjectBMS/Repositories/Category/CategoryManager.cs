using GraduationProjectBMS.Services;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectBMS.Repositories.Category
{
    public class CategoryManager : ICategoryManager
    {
        private readonly MyDbContext dbContext;
        public CategoryManager(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        void ICategoryManager.CreateCategory(Models.Category category)
        {
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
        }

        void ICategoryManager.DeleteCategory(int id)
        {
            var targetCategory = (from x in dbContext.Categories where x.CategoryId == id select x).FirstOrDefault();
            dbContext.Categories.Remove(targetCategory);
            dbContext.SaveChanges();
        }

        List<Models.Category> ICategoryManager.GetCategories()
        {
            return dbContext.Categories.ToList();
        }

        async Task<Models.Category> ICategoryManager.GetCategoryWithArticlesAsync(int categoryId)
        {
            return await dbContext.Categories
                             .Include(c => c.Articles)
                             .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }
    }
}
