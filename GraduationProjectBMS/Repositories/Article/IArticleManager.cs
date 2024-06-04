using GraduationProjectBMS.Models;

namespace GraduationProjectBMS
{
    public interface IArticleManager
    {
        public List<Article> GetArticles();
        public List<Article> GetUserArticles(string id);
        public void CreateArticle(Article article);
        public void DeleteArticle(int id);
        public void EditArticle(int id, Article article);
        public Article GetArticle(int id);
        public Article GetArticleByTitle(string ArticleTitle);
        public int GetArticleLikes(int id);
        public List<Article> GetSearchArticles(string query);
        public List<Comment> GetComments(int id);
    }
}
