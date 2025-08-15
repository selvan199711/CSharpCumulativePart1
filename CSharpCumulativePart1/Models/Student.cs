using System;

namespace CSharpCumulativePart2.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public string Program { get; set; } = string.Empty;
    }
}
