using LessonRegistration.Data;
using LessonRegistration.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LessonRegistration.Controllers
{
    [Route("api/Department")]
    public class DepartmentController : Controller
    {
        private readonly AppDBContext appDBContext;

        public DepartmentController(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        [HttpGet]
        public IEnumerable<DTO.Department> GetAll()
        {
            return IncludeAll(appDBContext.Departments)
                    .Select(d => new DTO.Department(d));
        }

        [HttpPost]
        public IActionResult Add([FromBody] DTO.DepartmentShort department)
        {
            var errorAction = GetDtoInvalidActionResult(department);
            if (errorAction != null)
            {
                return errorAction;
            }
            if (department.Name == null)
            {
                return BadRequest($"property {nameof(department.Name)} required");
            }
            if (department.Institute == null)
            {
                return BadRequest($"property institute required");
            }
            var institute = appDBContext.Institutes.Find(department.Institute.Id);
            if (institute == null)
            {
                return BadRequest($"cant find institute with id {department.Institute.Id}. Create it first");
            }

            var departmentDb = department.ToModel();
            departmentDb.Institute = institute;
            appDBContext.Add(departmentDb);

            appDBContext.SaveChanges();

            return Ok(new DTO.Department(departmentDb));
        }

        [HttpDelete("{id}")]
        public IActionResult Remove([FromRoute] int id)
        {
            var department = appDBContext.Departments
                .Include(d => d.Semesters)
                .FirstOrDefault(d => d.Id == id);
            if (department == null)
            {
                return NotFound($"department with id {id} not found");
            }
            if (department.Semesters.Any())
            {
                return BadRequest($"cascade delete prohibited. Remove all semesters first");
            }

            appDBContext.Departments.Remove(department);
            appDBContext.SaveChanges();

            return Ok(new DTO.Department(department));
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var department = appDBContext.Departments.Include(d => d.Institute).FirstOrDefault(d => d.Id == id);
            if (department == null)
            {
                return NotFound($"department with id {id} not found");
            }
            return Ok(new DTO.Department(department));
        }

        [HttpGet("search")]
        public IActionResult GetByName([FromQuery] string name)
        {
            var departments = appDBContext.Departments
                .Include(d => d.Institute)
                .Where(d => d.Name == name);

            return Ok(departments.Select(d => new DTO.Department(d)));
        }

        [HttpPut]
        public IActionResult Update([FromBody] DTO.DepartmentShort department)
        {
            var errorAction = GetDtoInvalidActionResult(department);
            if (errorAction != null)
            {
                return errorAction;
            }
            if (department.Name == null)
            {
                return BadRequest($"property {nameof(department.Name)} required");
            }
            var oldDepartment = appDBContext.Departments
                .Include(d => d.Institute)
                .FirstOrDefault(d => d.Id == department.Id);
            if (oldDepartment == null)
            {
                return NotFound($"department with id {department.Id} not found");
            }
            if (department.Institute == null)
            {
                return BadRequest($"property institute required");
            }
            var newInstitute = appDBContext.Institutes.Find(department.Institute.Id);
            if (newInstitute == null)
            {
                return BadRequest($"cant find institute with id {department.Institute.Id}. Create it first");
            }

            var newDepartment = department.ToModel();
            newDepartment.Institute = newInstitute;
            appDBContext.Entry(oldDepartment).State = EntityState.Detached;
            appDBContext.Departments.Update(newDepartment);
            appDBContext.SaveChanges();

            return Ok(new DTO.Department(newDepartment));
        }

        private ActionResult? GetDtoInvalidActionResult(DTO.DepartmentShort department)
        {
            if (department == null)
            {
                return BadRequest("cant map request body to DTO model");
            }

            return null;
        }

        private IQueryable<Data.Department> IncludeAll(IQueryable<Data.Department> departments)
            => departments
                .Include(d => d.Institute)
                .Include(d => d.Semesters);
    }
}
