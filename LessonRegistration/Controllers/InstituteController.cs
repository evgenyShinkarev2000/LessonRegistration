using LessonRegistration.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.OpenApi.Validations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LessonRegistration.Controllers
{
    [Route("api/Institute")]
    public class InstituteController : Controller
    {
        private readonly AppDBContext appDBContext;

        public InstituteController(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        [HttpGet]
        public IEnumerable<DTO.Institute> GetAll()
        {
            return appDBContext.Institutes
                .Include(i => i.Departments)
                .Select(i => new DTO.Institute(i));
        }

        [HttpPost]
        public IActionResult Add([FromBody] DTO.InstituteShort institute)
        {
            var errorAction = GetDtoInvalidActionResult(institute);
            if (errorAction != null)
            {
                return errorAction;
            }
            var instituteDb = institute.ToModel();
            appDBContext.Institutes.Add(instituteDb);
            appDBContext.SaveChanges();

            return Ok(new DTO.Institute(instituteDb));
        }

        [HttpDelete("{id}")]
        public IActionResult Remove([FromRoute] int id)
        {
            var removed = appDBContext.Institutes
                .Include(i => i.Departments)
                .FirstOrDefault(i => i.Id == id);
            if (removed == null)
            {
                return NotFound($"institute with id {id} not found");
            }
            if (removed.Departments.Any())
            {
                return BadRequest($"cascade delete prohibited. Remove all departments first");
            }
            appDBContext.Institutes.Remove(removed);
            appDBContext.SaveChanges();

            return Ok(new DTO.Institute(removed));
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var institute = appDBContext.Institutes.Include(i => i.Departments).FirstOrDefault(i => i.Id == id);
            if (institute == null)
            {
                return NotFound($"institute with id {id} not found");
            }

            return Ok(new DTO.Institute(institute));
        }

        [HttpGet("search")]
        public IActionResult GetByName([FromQuery] string name)
        {
            var institutes = appDBContext.Institutes
                .Include(i => i.Departments)
                .Where(i => i.Name == name);

            return Ok(institutes.Select(i => new DTO.Institute(i)));
        }

        [HttpPut]
        public IActionResult Update([FromBody] DTO.InstituteShort institute)
        {
            var errorAction = GetDtoInvalidActionResult(institute);
            if (errorAction != null)
            {
                return errorAction;
            }

            var oldInstitute = appDBContext.Institutes
                .Include(i => i.Departments)
                .FirstOrDefault(i => i.Id == institute.Id);
            if (oldInstitute == null)
            {
                return NotFound("can't update non-existent institute");
            }

            var newInstitute = institute.ToModel();
            appDBContext.Entry(oldInstitute).State = EntityState.Detached;
            appDBContext.Institutes.Update(newInstitute);
            
            appDBContext.SaveChanges();

            return Ok(new DTO.Institute(newInstitute));
        }

        private ActionResult? GetDtoInvalidActionResult(DTO.InstituteShort institute)
        {
            if (institute == null)
            {
                return BadRequest("cant map request body to DTO model");
            }

            return null;
        }
    }
}
