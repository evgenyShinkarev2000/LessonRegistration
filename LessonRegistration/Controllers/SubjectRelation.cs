using LessonRegistration.Data;
using LessonRegistration.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace LessonRegistration.Controllers
{
    [Route("api/Subject")]
    [Tags("Subject")]
    public class SubjectRelationController : Controller
    {
        private readonly AppDBContext appDBContext;

        public SubjectRelationController(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        [HttpPut("{id}/Semester")]
        public IActionResult UpdateSemester([FromRoute] int id, [FromBody] IEnumerable<DTO.SemesterShort> semesters)
        {
            var subject = appDBContext.Subjects
                .Include(s => s.UsedInSemesters) // если закомментировать, add ругается на дубликаты
                .FirstOrDefault(s => s.Id == id);
            if (semesters == null)
            {
                return BadRequest("cant map request body to IEnumerable<DTO.SemesterShort>");
            }
            if (subject == null)
            {
                return NotFound($"semester with id {id} not found");
            }

            subject.UsedInSemesters = new HashSet<Data.Semester>();
            foreach (var semester in semesters)
            {
                var dbSemester = appDBContext.Semesters
                    .Include(s => s.Department)
                    .ThenInclude(d => d.Institute)
                    .FirstOrDefault(s => s.Id == semester.Id);
                if (dbSemester == null)
                {
                    return BadRequest($"subject with id {semester.Id} not found. Create it first");
                }
                subject.UsedInSemesters.Add(dbSemester);
            }
            appDBContext.SaveChanges();

            return Ok(subject.UsedInSemesters.Select(s => new DTO.Semester(s)));
        }

        [HttpPost("{id}/Register")]
        public IActionResult RegisterStudent([FromRoute] int id, [FromBody] DTO.StudentShort student)
        {
            var subject = appDBContext.Subjects
                .Include(s => s.RegisteredStudents)
                .Include(s => s.UsedInSemesters)
                .FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return BadRequest("can't map request body to DTO.StudentShort");
            }
            if (subject == null)
            {
                return NotFound($"subject with id {id} not found. Create it first");
            }
            if (subject.Capacity <= 0)
            {
                return BadRequest("subject hasn't capacity");
            }

            var dbStudent = appDBContext.Students
                .Include(s => s.SelectedSubject)
                .Include(s => s.Semester)
                .FirstOrDefault(s => s.Id == student.Id);
            if (dbStudent == null)
            {
                return NotFound($"student with id {id} not found. Create it first");
            }
            if (!subject.UsedInSemesters.Contains(dbStudent.Semester))
            {
                return BadRequest($"subject with id {subject.Id} not available for students from semester with id {dbStudent.Semester.Id}");
            }
            if (subject.RegisteredStudents.Contains(dbStudent))
            {
                return Conflict("alredy registered");
            }

            if (subject.Capacity > subject.RegisteredStudents.Count())
            {
                subject.RegisteredStudents.Add(dbStudent);
                appDBContext.SaveChanges();

                return Ok("registered");
            }
            else
            {
                var dumbiestStudent = subject.RegisteredStudents.MinBy(s => s.AverageGrade)!;
                if (dumbiestStudent.AverageGrade < dbStudent.AverageGrade)
                {
                    subject.RegisteredStudents.Remove(dumbiestStudent);
                    subject.RegisteredStudents.Add(dbStudent);
                    appDBContext.SaveChanges();

                    return Ok("registered");
                }

                return Conflict("not enough points");
            }
        }

        [HttpPost("{id}/Unregister")]
        public IActionResult UnregisterStudent([FromRoute] int id, [FromBody] DTO.StudentShort student)
        {
            var subject = appDBContext.Subjects
          .Include(s => s.RegisteredStudents)
          .Include(s => s.UsedInSemesters)
          .FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return BadRequest("can't map request body to DTO.StudentShort");
            }
            if (subject == null)
            {
                return NotFound($"subject with id {id} not found. Create it first");
            }
            if (subject.Capacity <= 0)
            {
                return BadRequest("subject hasn't capacity");
            }

            var dbStudent = appDBContext.Students
                .Include(s => s.SelectedSubject)
                .Include(s => s.Semester)
                .FirstOrDefault(s => s.Id == student.Id);
            if (dbStudent == null)
            {
                return NotFound($"student with id {id} not found. Create it first");
            }
            if (!subject.UsedInSemesters.Contains(dbStudent.Semester))
            {
                return BadRequest($"subject with id {subject.Id} not available for students from semester with id {dbStudent.Semester.Id}");
            }
            if (!subject.RegisteredStudents.Contains(dbStudent))
            {
                return Conflict("not registered yet");
            }

            subject.RegisteredStudents.Remove(dbStudent);
            appDBContext.SaveChanges();

            return Ok("unregistered");
        }
    }
}
