using CSharpCumulativePart1.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CourseAPIController : ControllerBase
{
    private readonly SchoolDbContext _context;

    public CourseAPIController(SchoolDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllCourses()
    {
        return Ok(_context.Courses.ToList());
    }
}
