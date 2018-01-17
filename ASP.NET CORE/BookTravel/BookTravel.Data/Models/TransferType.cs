using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookTravel.Data.Models
{
    public class TransferType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public double Price { get; set; }

        public int MaxPeopleCount { get; set; }

        public bool IsOneWay { get; set; }

        public ICollection<Transfer> Transfers { get; set; } = new List<Transfer>();

    }
}
