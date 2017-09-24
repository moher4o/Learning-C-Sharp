using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace OneToMany.Models
{
   public class Person
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public int? ManagerId { get; set; }

        public Person Manager { get; set; }

        public ICollection<Person> Subordinates { get; set; } = new List<Person>();

    }
}
