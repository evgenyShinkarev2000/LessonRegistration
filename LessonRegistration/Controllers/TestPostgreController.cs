using LessonRegistration.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LessonRegistration.Controllers
{
    [Route("api/TestPostgre")]
    public class TestPostgreController : Controller
    {
        private readonly AppDBContext appDBContext;
        public TestPostgreController(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }
        [HttpPost]
        public IActionResult AddBBBToAAAList()
        {
            return StatusCode(404);

            //var b1 = appDBContext.BBBs.Find(1);
            //var b2 = appDBContext.BBBs.Find(2);
            //var a1 = appDBContext.AAAs.Find(1);
            //var a2 = appDBContext.AAAs.Find(2);
            //a1.BBBs?.Add(b2);
            //a2.BBBs?.Add(b1);

            //appDBContext.SaveChanges();

            //return Ok();
        }
    }
}
