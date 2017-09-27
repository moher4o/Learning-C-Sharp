namespace StudentSystem.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public ICollection<StudentCourse> Students { get; set; } = new HashSet<StudentCourse>();

        public ICollection<Resource> Resources { get; set; } = new HashSet<Resource>();

        public ICollection<HomeWork> Homeworks { get; set; } = new HashSet<HomeWork>();
    }
}
