using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManyToMany
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<StudentsCourses> StudentCourses { get; set; } = new List<StudentsCourses>();
    }
}
