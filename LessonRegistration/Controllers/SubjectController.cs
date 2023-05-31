using LessonRegistration.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LessonRegistration.Controllers
{
    [Route("api/Subject")]
    public class SubjectController : Controller
    {
        private readonly AppDBContext appDBContext;

        public SubjectController(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        [HttpGet]
        public IEnumerable<DTO.Subject> GetAll()
        {
            return appDBContext.Subjects
                .Include(s => s.RegisteredStudents)
                .ThenInclude(s => s.Semester)
                .ThenInclude(s => s.Department)
                .ThenInclude(d => d.Institute)
                .Include(s => s.UsedInSemesters)
                .ThenInclude(s => s.Department)
                .ThenInclude(d => d.Institute)
                .Select(s => new DTO.Subject(s));
        }

        
        [HttpPost]
        public IActionResult Add([FromBody] DTO.SubjectShort subject)
        {
            var errorAction = GetDtoInvalidActionResult(subject);
            if (errorAction != null)
            {
                return errorAction;
            }
            var subjectDb = subject.ToModel();
            appDBContext.Subjects.Add(subjectDb);
            appDBContext.SaveChanges();

            return Ok(new DTO.Subject(subjectDb));
        }

        [HttpDelete("{id}")]
        public IActionResult Remove([FromRoute] int id)
        {
            var removed = appDBContext.Subjects
                .FirstOrDefault(s => s.Id == id);
            if (removed == null)
            {
                return NotFound($"subject with id {id} not found");
            }

            appDBContext.Subjects.Remove(removed);
            appDBContext.SaveChanges();

            return Ok(new DTO.Subject(removed));
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var subject = appDBContext.Subjects
                .Include(s => s.RegisteredStudents)
                .Include(s => s.UsedInSemesters)
                .FirstOrDefault(i => i.Id == id);
            if (subject == null)
            {
                return NotFound($"subject with id {id} not found");
            }

            return Ok(new DTO.Subject(subject));
        }

        [HttpGet("search")]
        public IActionResult GetByName([FromQuery] string name)
        {
            var subjects = appDBContext.Subjects
                .Include(s => s.UsedInSemesters)
                .Include(s => s.RegisteredStudents)
                .Where(s => s.Name == name);

            return Ok(subjects.Select(s => new DTO.Subject(s)));
        }

        [HttpPut]
        public IActionResult Update([FromBody] DTO.SubjectShort subject)
        {
            var errorAction = GetDtoInvalidActionResult(subject);
            if (errorAction != null)
            {
                return errorAction;
            }

            var oldSubject = appDBContext.Subjects
                .Include(s => s.RegisteredStudents)
                .Include(s => s.UsedInSemesters)
                .FirstOrDefault(i => i.Id == subject.Id);
            if (oldSubject == null)
            {
                return NotFound("can't update non-existent institute");
            }
            if (oldSubject.RegisteredStudents.Count() < subject.Capacity)
            {
                return BadRequest("can't set capacity less registered students count. Remove registered students first.");
            }

            var newSubject = subject.ToModel();
            appDBContext.Entry(oldSubject).State = EntityState.Detached;
            appDBContext.Subjects.Update(newSubject);

            appDBContext.SaveChanges();

            return Ok(new DTO.Subject(newSubject));
        }


        private ActionResult? GetDtoInvalidActionResult(object institute)
        {
            if (institute == null)
            {
                return BadRequest("cant map request body to DTO model");
            }

            return null;
        }
    }
}
