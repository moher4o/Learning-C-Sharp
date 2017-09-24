using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManyToMany
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<StudentsCourses> Participants { get; set; } = new List<StudentsCourses>();

    }
}
