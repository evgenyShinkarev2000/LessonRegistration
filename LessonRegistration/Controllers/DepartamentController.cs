using LessonRegistration.Data.Models;
using LessonRegistration.Data.Services;
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
        private readonly Departments departments;
        private readonly Institutes institutes;

        public DepartmentController(Departments departments, Institutes institutes)
        {
            this.departments = departments;
            this.institutes = institutes;
        }

        [HttpGet]
        public IEnumerable<DTO.Department> GetAll()
        {
            return departments.GetAll()
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
            var institute = institutes.GetById(department.Institute.Id);
            if (institute == null)
            {
                return BadRequest($"cant find institute with id {department.Institute.Id}. Create it first");
            }

            var newDepartment = departments.Add(department.ToModel());

            return Ok(new DTO.Department(newDepartment));
        }

        [HttpDelete("{id}")]
        public IActionResult Remove([FromRoute] int id)
        {
            var department = departments.GetById(id);
            if (department == null)
            {
                return NotFound($"department with id {id} not found");
            }
            if (department.Semesters.Any())
            {
                return BadRequest($"cascade delete prohibited. Remove all semesters first");
            }

            departments.Remove(department);

            return Ok(new DTO.Department(department));
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var department = departments.GetById(id);
            if (department == null)
            {
                return NotFound($"department with id {id} not found");
            }
            return Ok(new DTO.Department(department));
        }

        [HttpGet("search")]
        public IActionResult GetByName([FromQuery] string name)
        {
            var departments = this.departments.FindByName(name);

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
            var oldDepartment = departments.GetById(department.Id);
            if (oldDepartment == null)
            {
                return NotFound($"department with id {department.Id} not found");
            }
            if (department.Institute == null)
            {
                return BadRequest($"property institute required");
            }
            var institute = institutes.GetById(department.Institute.Id);
            if (institute == null)
            {
                return BadRequest($"cant find institute with id {department.Institute.Id}. Create it first");
            }

            var newDepartment = departments.Update(department.ToModel());

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
    }
}
