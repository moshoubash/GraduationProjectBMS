using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectBMS.Controllers
{
    public class LikeController : Controller
    {
        private readonly ILikeManager _likeManager;
        public LikeController(ILikeManager _likeManager)
        {
            this._likeManager = _likeManager;
        }

        [HttpGet]
        public ActionResult GetLikes(int articleId) {
            return View(_likeManager.GetLikes(articleId));
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddLike(int articleId)
        {
            try {
                _likeManager.AddLike(articleId);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult RemoveLike(int articleId)
        {
            try
            {
                _likeManager.RemoveLike(articleId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
