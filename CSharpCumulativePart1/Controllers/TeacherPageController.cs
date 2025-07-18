using Microsoft.AspNetCore.Mvc;
using CSharpCumulativePart1.Models;

namespace CSharpCumulativePart1.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly SchoolDbContext _context;
        public TeacherPageController(SchoolDbContext context) { _context = context; }

        public IActionResult List(DateTime? startDate, DateTime? endDate)
        {
            var teachers = _context.Teachers.AsQueryable();

            if (startDate.HasValue && endDate.HasValue)
            {
                teachers = teachers.Where(t => t.HireDate >= startDate && t.HireDate <= endDate);
            }

            return View(teachers.ToList());
        }


        //public IActionResult Show(int id)
        //{
        //    var teacher = _context.Teachers.Find(id);
        //    if (teacher == null)
        //        return NotFound();
        //    return View(teacher);
        //}

        public IActionResult Show(int id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher == null)
                return NotFound();

            var courses = _context.Courses
                .Where(c => c.TeacherId == id)
                .ToList();

            var viewModel = new TeacherWithCoursesViewModel
            {
                Teacher = teacher,
                Courses = courses
            };

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult SearchByHireDate(DateTime startDate, DateTime endDate)
        {
            var results = _context.Teachers
                .Where(t => t.HireDate >= startDate && t.HireDate <= endDate)
                .ToList();

            return View("List", results); // Reuse same List view
        }

    }
}
