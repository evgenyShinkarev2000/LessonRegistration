using LessonRegistration.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LessonRegistration.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly AppDBContext appDBContext;

        public UserController(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
