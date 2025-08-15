using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CSharpCumulativePart2.Models;

namespace CSharpCumulativePart2.Controllers
{
    public class StudentPageController : Controller
    {
        private readonly SchoolDbContext _db;
        public StudentPageController(SchoolDbContext db) => _db = db;

        public async Task<IActionResult> List()
        {
            var students = await _db.ListStudentsAsync();
            return View(students);
        }
    }
}
