using System.Collections.Generic;

namespace CSharpCumulativePart2.Models
{
    public sealed partial class SchoolDbContext
    {
        public List<Student> Students => new();
        public List<Course> Courses => new();
    }
}
