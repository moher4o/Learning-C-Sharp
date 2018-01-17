using System.ComponentModel.DataAnnotations;

namespace BookTravel.Web.Areas.Admin.Models.TransferTypes
{
    public class AddTransferTypeViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Range(0,100000)]
        public double Price { get; set; }

        [Range(0,1000)]
        [Display(Name ="Max People Count")]
        public int MaxPeopleCount { get; set; }

        [Display(Name = "Is it one way")]
        public bool IsOneWay { get; set; } = false;

    }
}
