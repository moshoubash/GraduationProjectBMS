using GraduationProjectBMS.Models;

namespace GraduationProjectBMS.Repositories.User
{
    public interface IUserFunctions
    {
        public void CreateArticle(Article article);
        public void DeleteArticle(int id);
        public void EditArticle(int id, Article article);
        public void ToggleLike(int id);
        public List<Article> GetUserArticles(string UserId);
    }
}
