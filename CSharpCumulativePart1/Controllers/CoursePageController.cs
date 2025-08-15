using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CSharpCumulativePart2.Models;

namespace CSharpCumulativePart2.Controllers
{
    public class CoursePageController : Controller
    {
        private readonly SchoolDbContext _db;
        public CoursePageController(SchoolDbContext db) => _db = db;

        public async Task<IActionResult> List()
        {
            var courses = await _db.ListCoursesAsync();
            return View(courses);
        }
    }
}
