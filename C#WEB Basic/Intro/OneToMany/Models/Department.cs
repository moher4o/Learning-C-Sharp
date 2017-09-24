using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneToMany.Models
{
   public class Department
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Person> Employes { get; set; } = new List<Person>();
    }
}
