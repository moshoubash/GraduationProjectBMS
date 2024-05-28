using GraduationProjectBMS.Models;
using GraduationProjectBMS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectBMS.Repositories.User
{
    public class UserFunctions : IUserFunctions
    {
        private readonly MyDbContext dbContext;
        private readonly UserManager<AppUser> userManager;
        public UserFunctions(MyDbContext dbContext, UserManager<AppUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        void IUserFunctions.ToggleLike(int id)
        {
            
        }

        void IUserFunctions.CreateArticle(Article article)
        {
            dbContext.Articles.Add(article);
            dbContext.SaveChanges();
        }

        void IUserFunctions.DeleteArticle(int id)
        {
            var targetArticle = (from article in dbContext.Articles where article.ArticleId == id select article).FirstOrDefault();
            dbContext.Articles.Remove(targetArticle);
            dbContext.SaveChanges();
        }

        void IUserFunctions.EditArticle(int id, Article article)
        {
            var targetArticle = (from x in dbContext.Articles where x.ArticleId == id select x).FirstOrDefault();
            targetArticle.ArticleTitle = article.ArticleTitle;
            targetArticle.ArticleContent = article.ArticleContent;
            targetArticle.ArticleThumbnail = article.ArticleThumbnail;
            dbContext.SaveChanges();
        }

        List<Article> IUserFunctions.GetUserArticles(string UserId)
        {
            return dbContext.Articles.Where(article => article.Id == UserId).ToList();
        }
    }
}
