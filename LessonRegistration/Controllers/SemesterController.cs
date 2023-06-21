using LessonRegistration.Data.Models;
using LessonRegistration.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LessonRegistration.Controllers
{
    [Route("api/Semester")]
    public class SemesterController : Controller
    {
        private readonly AppDBContext appDBContext;
        public SemesterController(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        [HttpGet]
        public IEnumerable<DTO.Semester> GetAll()
        {
            return IncludeAll(appDBContext.Semesters).Select(d => new DTO.Semester(d));
        }

        [HttpPost]
        public IActionResult Add([FromBody] DTO.SemesterShort semester)
        {
            var errorAction = GetDtoInvalidActionResult(semester);
            if (errorAction != null)
            {
                return errorAction;
            }
            if (semester.Department == null)
            {
                return BadRequest($"semester must contain {nameof(semester.Department)}");
            }
            var trackedDepartment = appDBContext.Departments
                .Include(d => d.Institute)
                .FirstOrDefault(d => d.Id == semester.Department.Id);
            if (trackedDepartment == null)
            {
                return BadRequest($"cant find department with id {semester.Department.Id}. For adding semester create it first");
            }
            var semesterDb = semester.ToModel();
            semesterDb.Department = trackedDepartment;
            appDBContext.Semesters.Add(semesterDb);

            appDBContext.SaveChanges();

            return Ok(new DTO.Semester(semesterDb));
        }

        [HttpDelete("{id}")]
        public IActionResult Remove([FromRoute] int id)
        {
            var removed = IncludeAll(appDBContext.Semesters)
                .Include(s => s.Department)
                .Include(s => s.Students)
                .Include(s => s.Subjects)
                .FirstOrDefault(i => i.Id == id);
            if (removed == null)
            {
                return NotFound($"semester with id {id} not found");
            }
            if (removed.Students.Any())
            {
                return BadRequest($"cascade delete prohibited. Remove all students first");
            }
            appDBContext.Semesters.Remove(removed);
            appDBContext.SaveChanges();

            return Ok(new DTO.Semester(removed));
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var semester = appDBContext.Semesters
                .Include(s => s.Department)
                .Include(s => s.Students)
                .Include(s => s.Subjects)
                .FirstOrDefault(d => d.Id == id);
            if (semester == null)
            {
                return NotFound($"semester with id {id} not found");
            }

            return Ok(new DTO.Semester(semester));
        }

        [HttpGet("search")]
        public IActionResult GetByQuery([FromQuery] int semesterNumber)
        {
            var semesters = this.appDBContext.Semesters
                .Include(s => s.Department)
                .Include(s => s.Students)
                .Include(s => s.Subjects)
                .Where(s => s.SemesterNumber == semesterNumber);

            return Ok(semesters);
        }

        [HttpPut]
        public IActionResult Update([FromBody] DTO.SemesterShort semester)
        {
            var errorAction = GetDtoInvalidActionResult(semester);
            if (errorAction != null)
            {
                return errorAction;
            }
            var oldSemester = appDBContext.Semesters.Find(semester.Id);
            if (oldSemester == null)
            {
                return NotFound($"can't find semester with id {semester.Id}");
            }
            if (semester.Department == null)
            {
                return BadRequest($"semester must contain {nameof(semester.Department)}");
            }
            var department = appDBContext.Departments
                .Include(d => d.Institute)
                .FirstOrDefault(d => d.Id == semester.Department.Id);
            if (department == null)
            {
                return BadRequest($"cant find department with id {semester.Department.Id}. Create it first for updating semester");
            }

            var newSemester = semester.ToModel();
            newSemester.Department = department;
            appDBContext.Entry(oldSemester).State = EntityState.Detached;
            appDBContext.Semesters.Update(newSemester);
            appDBContext.SaveChanges();

            return Ok(new DTO.Semester(newSemester));
        }

        private ActionResult? GetDtoInvalidActionResult(DTO.SemesterShort semester)
        {
            if (semester == null)
            {
                return BadRequest("cant map request body to DTO model");
            }

            return null;
        }

        private IQueryable<Data.Models.Semester> IncludeAll(IQueryable<Data.Models.Semester> semesters)
             => semesters
                .Include(s => s.Department)
                .Include(s => s.Students)
                .Include(s => s.Subjects)
                .Include(s => s.Department.Institute);
    }
}
