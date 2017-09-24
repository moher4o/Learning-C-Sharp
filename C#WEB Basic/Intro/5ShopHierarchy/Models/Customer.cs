using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopHierarchy5.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int? SalesmanId { get; set; }

        public Salesman Salesman { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
