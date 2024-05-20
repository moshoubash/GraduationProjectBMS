using GraduationProjectBMS.Models;
using GraduationProjectBMS.Services;

namespace GraduationProjectBMS
{
    public class ArticleManager : IArticleManager
    {
        private readonly MyDbContext _dbContext;
        public ArticleManager(MyDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        void IArticleManager.CreateArticle(Article article)
        {
            _dbContext.Articles.Add(article);
            _dbContext.SaveChanges();
        }

        void IArticleManager.DeleteArticle(int id)
        {
            var targetArticle = (from x in _dbContext.Articles where x.ArticleId == id select x).FirstOrDefault();
            _dbContext.Articles.Remove(targetArticle);
            _dbContext.SaveChanges();
        }

        void IArticleManager.EditArticle(int id, Article article)
        {
            var targetArticle = (from x in _dbContext.Articles where x.ArticleId == id select x).FirstOrDefault();
            targetArticle.ArticleTitle = article.ArticleTitle;
            targetArticle.ArticleContent = article.ArticleContent;
            targetArticle.ArticleDescription = article.ArticleDescription;
            targetArticle.ArticleThumbnail = article.ArticleThumbnail;

            _dbContext.SaveChanges();
        }

        Article IArticleManager.GetArticle(int id)
        {
            var targetArticle = (from x in _dbContext.Articles where x.ArticleId == id select x).FirstOrDefault();
            return targetArticle;
        }

        List<Article> IArticleManager.GetArticles()
        {
            return _dbContext.Articles.ToList();
        }
    }
}
