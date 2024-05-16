using GraduationProjectBMS.Models.System_Models;
namespace GraduationProjectBMS.Repositories.Article
{
    public interface IArticleManager
    {
        public List<Models.System_Models.Article> GetArticles();
        public void CreateArticle(Models.System_Models.Article article);
        public void DeleteArticle(int id);
        public void EditArticle(int id, Models.System_Models.Article article);
        public Models.System_Models.Article GetArticle(int id);
    }
}
