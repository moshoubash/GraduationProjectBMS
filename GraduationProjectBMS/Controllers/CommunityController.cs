using GraduationProjectBMS.Models;
using GraduationProjectBMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectBMS.Controllers
{
    public class CommunityController : Controller
    {
        //Soon...
        private readonly MyDbContext dbContext;
        public CommunityController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: CommunityController
        public ActionResult Chat()
        {
            return View();
        }
    }
}
