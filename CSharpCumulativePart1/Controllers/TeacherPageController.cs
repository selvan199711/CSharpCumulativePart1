using Microsoft.AspNetCore.Mvc;
using CSharpCumulativePart1.Models;
using Microsoft.EntityFrameworkCore;

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

        // Display the form
        public IActionResult New()
        {
            return View();
        }

        // Receive form and add teacher
        [HttpPost]
        public IActionResult New(Teacher teacher)
        {
            if (string.IsNullOrWhiteSpace(teacher.FirstName) || string.IsNullOrWhiteSpace(teacher.LastName))
            {
                ViewBag.Error = "Name is required.";
                return View(teacher);
            }

            if (teacher.HireDate > DateTime.Now)
            {
                ViewBag.Error = "Hire Date can't be in the future.";
                return View(teacher);
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(teacher.EmployeeNumber ?? "", @"^T\d+$"))
            {
                ViewBag.Error = "Employee Number must start with 'T' and digits.";
                return View(teacher);
            }

            if (_context.Teachers.Any(t => t.EmployeeNumber == teacher.EmployeeNumber))
            {
                ViewBag.Error = "Employee Number already exists.";
                return View(teacher);
            }

            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        // Confirm delete
        public IActionResult DeleteConfirm(int id)
        {
            var teacher = _context.Teachers.Find(id);
            return teacher == null ? NotFound() : View(teacher);
        }

        // Handle delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("DELETE POST HIT for ID: " + id);
            var teacher = _context.Teachers.Find(id);
            if (teacher == null)
                return NotFound();

            try
            {
                _context.Teachers.Remove(teacher);
                _context.SaveChanges();
                TempData["Message"] = "Teacher deleted successfully.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "Can't delete: Teacher is assigned to courses.";
            }

            return RedirectToAction("List");
        }



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
