using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CSharpCumulativePart2.Models;

namespace CSharpCumulativePart2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseAPIController : ControllerBase
    {
        private readonly SchoolDbContext _db;
        public CourseAPIController(SchoolDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _db.ListCoursesAsync());
    }
}
