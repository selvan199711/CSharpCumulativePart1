using System;
using System.ComponentModel.DataAnnotations;

namespace CSharpCumulativePart2.Models
{
    /// <summary>One teacher row from the Teachers table.</summary>
    public class Teacher
    {
        /// <summary>DB primary key.</summary>
        public int TeacherId { get; set; }

        /// <summary>Given name.</summary>
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>Family name.</summary>
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>Staff/employee number (e.g., EMP123).</summary>
        [Required(ErrorMessage = "Employee number is required.")]
        public string EmployeeNumber { get; set; } = string.Empty;

        /// <summary>Date hired.</summary>
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        /// <summary>Annual salary (>= 0).</summary>
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be 0 or greater.")]
        public decimal Salary { get; set; }

        /// <summary>Optional work phone.</summary>
        public string? WorkPhone { get; set; }
    }
}
