using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopHierarchy5.Models
{
    public class Salesman
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Customer> Customers { get; set; } = new List<Customer>();

    }
}
