using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CSharpCumulativePart2.Models;

namespace CSharpCumulativePart1.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly SchoolDbContext _db;

        public TeacherPageController(SchoolDbContext db)
        {
            _db = db;
        }

        public IActionResult List() => View(); 

        public async Task<IActionResult> Edit(int id)
        {
            var t = await _db.FindTeacherAsync(id);
            if (t is null) return NotFound($"Teacher {id} not found");
            return View(t);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Teacher form)
        {
            var exists = await _db.FindTeacherAsync(form.TeacherId);
            if (exists is null) return NotFound($"Teacher {form.TeacherId} not found");

            if (string.IsNullOrWhiteSpace(form.FirstName) || string.IsNullOrWhiteSpace(form.LastName))
            {
                ModelState.AddModelError("", "First and last name are required.");
                return View(form);
            }

            await _db.UpdateTeacherAsync(form);
            TempData["Message"] = "Teacher updated.";
            return RedirectToAction(nameof(Edit), new { id = form.TeacherId });
        }
    }
}
