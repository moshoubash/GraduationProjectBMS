using GraduationProjectBMS.Models;
using GraduationProjectBMS.Services;

namespace GraduationProjectBMS
{
    public class LikeManager : ILikeManager
    {
        private readonly MyDbContext myDbContext;
        public LikeManager(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }
        void ILikeManager.AddLike(int articleId)
        {
            // artiles likes + 1
            // dbcontext.SaveChanges();
            throw new NotImplementedException();
        }

        int ILikeManager.GetLikes(int id)
        {
            /*return myDbContext.Articles.TotalLikes;*/
            throw new NotImplementedException();
        }

        void ILikeManager.RemoveLike(int articleId)
        {
            // artiles likes - 1
            throw new NotImplementedException();
        }
    }
}
