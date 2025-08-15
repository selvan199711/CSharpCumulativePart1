using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CSharpCumulativePart2.Models;

namespace CSharpCumulativePart2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentAPIController : ControllerBase
    {
        private readonly SchoolDbContext _db;
        public StudentAPIController(SchoolDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _db.ListStudentsAsync());
    }
}
