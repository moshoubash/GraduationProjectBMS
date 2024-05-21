using GraduationProjectBMS.Models;
namespace GraduationProjectBMS
{
    public interface ILikeManager
    {
        public int GetLikes(int id);
        public void AddLike(int articleId);
        public void RemoveLike(int articleId);
    }
}
