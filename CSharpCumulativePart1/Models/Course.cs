namespace CSharpCumulativePart2.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int? TeacherId { get; set; }
    }
}
