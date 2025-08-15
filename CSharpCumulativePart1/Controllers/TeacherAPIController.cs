using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CSharpCumulativePart2.Models;

namespace CSharpCumulativePart2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherAPIController : ControllerBase
    {
        private readonly SchoolDbContext _db;

        public TeacherAPIController(SchoolDbContext db) => _db = db;

        /// <summary>Get one teacher by id. Returns 200 or 404</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Teacher), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOne(int id)
        {
            var t = await _db.FindTeacherAsync(id);
            return t is null ? NotFound(new { message = $"Teacher {id} not found" }) : Ok(t);
        }

        /// <summary>Update a teacher. Path id must match body.TeacherId. Returns 200, 400, or 404</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Teacher), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(object), 404)]
        public async Task<IActionResult> Update(int id, [FromBody] Teacher payload)
        {
            if (payload is null) return BadRequest(new { message = "Body is required." });
            if (id != payload.TeacherId) return BadRequest(new { message = "Path id and body TeacherId must match." });

            var errors = ValidateTeacherForUpdate(payload);
            if (errors != null) return BadRequest(errors);

            var existing = await _db.FindTeacherAsync(id);
            if (existing is null) return NotFound(new { message = $"Teacher {id} does not exist." });

            var rows = await _db.UpdateTeacherAsync(payload);
            if (rows == 0) return NotFound(new { message = $"Teacher {id} not found (no rows updated)." });

            var updated = await _db.FindTeacherAsync(id);
            return Ok(updated);
        }

        private static object? ValidateTeacherForUpdate(Teacher t)
        {
            var errs = new System.Collections.Generic.Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(t.FirstName)) errs["firstName"] = "First name cannot be empty.";
            if (string.IsNullOrWhiteSpace(t.LastName)) errs["lastName"] = "Last name cannot be empty.";
            if (t.Salary < 0) errs["salary"] = "Salary must be 0 or greater.";
            if (t.HireDate.Date > DateTime.Today) errs["hireDate"] = "Hire date cannot be in the future.";

            return errs.Count > 0 ? new { errors = errs } : null;
        }
    }
}
