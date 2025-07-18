using System;
using System.ComponentModel.DataAnnotations;

namespace CSharpCumulativePart1.Models
{
    /// <summary>
    /// Represents a teacher in the school.
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// The unique identifier for the teacher.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The first name of the teacher.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the teacher.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The date the teacher was hired.
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// The employee number assigned to the teacher.
        /// </summary>
        public string EmployeeNumber { get; set; }

        /// <summary>
        /// The salary of the teacher.
        /// </summary>
        public decimal Salary { get; set; }
    }

}
