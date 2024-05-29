using GraduationProjectBMS.Repositories.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectBMS.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly IUserFunctions userFunctions;
        public UsersController(IUserFunctions userFunctions)
        {
            this.userFunctions = userFunctions;
        }
        // GET: UsersController
        public ActionResult List()
        {
            return View(userFunctions.GetUsers());
        }

        // GET: UsersController/Details/guid
        public ActionResult Details(string id)
        {
            return View(userFunctions.GetUser(id));
        }

        public ActionResult Delete(string id)
        {
            try
            {
                userFunctions.DeleteUser(id);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }
    }
}
