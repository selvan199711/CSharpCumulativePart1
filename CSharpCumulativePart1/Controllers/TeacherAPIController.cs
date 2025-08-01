using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CSharpCumulativePart1.Models;

namespace CSharpCumulativePart1.Controllers
{
    /// <summary>
    /// This controller handles requests for teacher data through the API.
    /// It lets you fetch all teachers or just one based on their ID.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        /// <summary>
        /// Sets up access to the database so this controller can pull teacher info.
        /// </summary>
        /// <param name="context">Used to connect to the School database.</param>
        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of every teacher in the system.
        /// </summary>
        /// <returns>A list of all teachers with their details (status 200 OK).</returns>
        [HttpGet]
        public IActionResult GetAllTeachers()
        {
            var teachers = _context.Teachers.ToList();
            return Ok(teachers);
        }

        /// <summary>
        /// Looks up a teacher using their ID number.
        /// </summary>
        /// <param name="id">The teacher’s ID to search for.</param>
        /// <returns>The teacher’s info if found (200 OK), or a 404 error if not.</returns>
        [HttpGet("{id}")]
        public IActionResult GetTeacherById(int id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher == null)
                return NotFound("Teacher not found.");
            return Ok(teacher);
        }


        /// Adds a new teacher using POST.
        [HttpPost]
        public IActionResult AddTeacher([FromBody] Teacher newTeacher)
        {
            if (string.IsNullOrWhiteSpace(newTeacher.FirstName) || string.IsNullOrWhiteSpace(newTeacher.LastName))
            {
                return BadRequest("First and Last Name are required.");
            }

            if (newTeacher.HireDate > DateTime.Now)
                return BadRequest("Hire Date can't be in the future.");

            if (!System.Text.RegularExpressions.Regex.IsMatch(newTeacher.EmployeeNumber ?? "", @"^T\d+$"))
                return BadRequest("Employee Number must start with 'T' followed by digits.");

            if (_context.Teachers.Any(t => t.EmployeeNumber == newTeacher.EmployeeNumber))
                return BadRequest("Employee Number already exists.");

            _context.Teachers.Add(newTeacher);
            _context.SaveChanges();
            return Ok("Teacher added.");
        }

        /// Deletes a teacher by their ID.
        [HttpDelete("{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher == null)
                return NotFound("Teacher not found.");

            _context.Teachers.Remove(teacher);
            _context.SaveChanges();
            return Ok("Teacher deleted.");
        }


    }
}
