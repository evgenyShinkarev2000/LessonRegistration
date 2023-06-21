using LessonRegistration.Data.Models;
using LessonRegistration.Data.Services;
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
        private readonly Institutes institutes;

        public InstituteController(Institutes institutes)
        {
            this.institutes = institutes;
        }

        [HttpGet]
        public IEnumerable<DTO.Institute> GetAll()
        {
            return institutes.GetAll().Select(i => new DTO.Institute(i));
        }

        [HttpPost]
        public IActionResult Add([FromBody] DTO.InstituteShort institute)
        {
            var errorAction = GetDtoInvalidActionResult(institute);
            if (errorAction != null)
            {
                return errorAction;
            }
            var instituteDb = institutes.Add(institute.ToModel());

            return Ok(new DTO.Institute(instituteDb));
        }

        [HttpDelete("{id}")]
        public IActionResult Remove([FromRoute] int id)
        {
            var removed = institutes.GetById(id);
            if (removed == null)
            {
                return NotFound($"institute with id {id} not found");
            }
            if (removed.Departments.Any())
            {
                return BadRequest($"cascade delete prohibited. Remove all departments first");
            }
            institutes.Remove(removed);

            return Ok(new DTO.Institute(removed));
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var institute = institutes.GetById(id);
            if (institute == null)
            {
                return NotFound($"institute with id {id} not found");
            }

            return Ok(new DTO.Institute(institute));
        }

        [HttpGet("search")]
        public IActionResult GetByName([FromQuery] string name)
        {
            var institutes = this.institutes.FindByName(name);

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

            var oldInstitute = institutes.GetById(institute.Id);
            if (oldInstitute == null)
            {
                return NotFound($"can't update non-existent institute with id {institute.Id}");
            }

            var newInstitute = institutes.Update(institute.ToModel());

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
