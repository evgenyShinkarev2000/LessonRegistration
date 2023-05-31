using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Net.Http;

namespace LessonRegistration.Controllers
{
    [Route("api/security")]
    public class SecurityController : Controller
    {
        [Authorize]
        [HttpGet("any")]
        public IActionResult Any()
        {
            dynamic obj = new ExpandoObject();
            obj.message = "you are authenticated";

            return Ok(obj);
        }

        [Authorize(Roles = "client-student")]
        [HttpGet("student")]
        public IActionResult Student()
        {
            dynamic obj = new ExpandoObject();
            obj.message = "you are authorized student";

            return Ok(obj);
        }

        [Authorize(Roles = "client-admin")]
        [HttpGet("admin")]
        public IActionResult Admin()
        {
            dynamic obj = new ExpandoObject();
            obj.message = "you are authorized admin";

            return Ok(obj);
        }
    }
}
