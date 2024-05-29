using GraduationProjectBMS.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace GraduationProjectBMS
{
    public interface ICategoryManager
    {
        List<Category> GetCategories();
        void DeleteCategory(int id);
        void CreateCategory(Category category);
    }
}
