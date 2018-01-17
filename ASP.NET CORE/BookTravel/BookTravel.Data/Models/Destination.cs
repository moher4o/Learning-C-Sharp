using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookTravel.Data.Models
{
    public class Destination
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Transfer> Transfers { get; set; } = new List<Transfer>();
    }
}
