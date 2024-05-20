using GraduationProjectBMS.Models;

namespace GraduationProjectBMS
{
    public interface IArticleManager
    {
        public List<Article> GetArticles();
        public void CreateArticle(Article article);
        public void DeleteArticle(int id);
        public void EditArticle(int id, Article article);
        public Article GetArticle(int id);
    }
}
