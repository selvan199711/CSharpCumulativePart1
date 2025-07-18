namespace CSharpCumulativePart1.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        // Foreign key for Teacher
        public int TeacherId { get; set; }
    }
}
