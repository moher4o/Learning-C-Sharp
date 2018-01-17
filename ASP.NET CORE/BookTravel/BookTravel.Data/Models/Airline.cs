using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookTravel.Data.Models
{
    public class Airline
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Transfer> TransferArrivals { get; set; } = new List<Transfer>();

        public ICollection<Transfer> TransferDepartures { get; set; } = new List<Transfer>();

    }
}
