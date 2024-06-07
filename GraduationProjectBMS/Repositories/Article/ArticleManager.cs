using GraduationProjectBMS.Models;
using GraduationProjectBMS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace GraduationProjectBMS
{
    public class ArticleManager : IArticleManager
    {
        private readonly MyDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        public ArticleManager(MyDbContext _dbContext, UserManager<AppUser> userManager)
        {
            this._dbContext = _dbContext;
            _userManager = userManager;

        }

        public List<Article> GetUserArticles(string id)
        {
            return _dbContext.Articles.Where(x=> x.Id == id).ToList();
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

        void IArticleManager.DeleteComment(int id)
        {
            var targetComment = (from x in _dbContext.Comments where x.CommentId == id select x).FirstOrDefault();
            _dbContext.Comments.Remove(targetComment);
            _dbContext.SaveChanges();
        }

        void IArticleManager.DeleteReply(int id)
        {
            var targetReply = (from x in _dbContext.Replies where x.ReplyId == id select x).FirstOrDefault();
            _dbContext.Replies.Remove(targetReply);
            _dbContext.SaveChanges();
        }

        void IArticleManager.EditArticle(int id, Article article)
        {
            var targetArticle = (from x in _dbContext.Articles where x.ArticleId == id select x).FirstOrDefault();
            targetArticle.ArticleTitle = article.ArticleTitle;
            targetArticle.ArticleContent = article.ArticleContent;
            targetArticle.ArticleThumbnail = article.ArticleThumbnail;

            _dbContext.SaveChanges();
        }

        List<string> IArticleManager.GenerateTags(string content)
        {
            // Simple keyword extraction logic
            var words = Regex.Split(content, @"\W+")
                             .Where(w => w.Length > 3) // Example: ignore words with less than 3 characters
                             .GroupBy(w => w.ToLower())
                             .OrderByDescending(g => g.Count())
                             .Take(10) // Get top 10 frequent words
                             .Select(g => g.Key)
                             .ToList();

            return words;
        }

        Article IArticleManager.GetArticle(int id)
        {
            var targetArticle = (from x in _dbContext.Articles where x.ArticleId == id select x).FirstOrDefault();
            return targetArticle;
        }

        Article IArticleManager.GetArticleByTitle(string ArticleTitle)
        {
            var targetArticle = (from article in _dbContext.Articles where article.ArticleTitle == ArticleTitle select article).FirstOrDefault();
            return targetArticle;
        }

        int IArticleManager.GetArticleLikes(int id)
        {
            return _dbContext.Likes.Where(x=>x.ArticleId == id).Count();
        }

        List<Article> IArticleManager.GetArticles()
        {
            return _dbContext.Articles.ToList();
        }

        List<Comment> IArticleManager.GetComments(int id)
        {
            return _dbContext.Comments.Where(x=>x.ArticleId == id).ToList();
        }

        List<Comment> IArticleManager.GetCommentsWithReplies(int articleId)
        {
            return _dbContext.Comments
                       .Where(c => c.ArticleId == articleId)
                       .Include(c => c.Replies)
                       .ToList();
        }

        List<Reply> IArticleManager.GetReplies(int Articleid, int CommentId)
        {
            return _dbContext.Replies.Where(x=> x.ArticleId == Articleid && x.CommentId == CommentId).ToList();
        }

        List<Article> IArticleManager.GetSearchArticles(string query)
        {
            return _dbContext.Articles.Where(x => x.ArticleTitle.Contains(query) || 
                                                  x.ArticleContent.Contains(query)).ToList();
        }
    }
}
