using CSharpCumulativePart1.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StudentAPIController : ControllerBase
{
    private readonly SchoolDbContext _context;

    public StudentAPIController(SchoolDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllStudents()
    {
        return Ok(_context.Students.ToList());
    }
}
