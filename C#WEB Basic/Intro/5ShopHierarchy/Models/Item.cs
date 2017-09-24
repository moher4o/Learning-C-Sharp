﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopHierarchy5.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public ICollection<ItemsOrders> Orders { get; set; } = new List<ItemsOrders>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
