using System.ComponentModel.DataAnnotations;

namespace BookTravel.Web.Areas.Admin.Models.Destinations
{
    public class NewDestinationViewModel
    {
        [Required]
        [MaxLength(100)]
        [Display(Name ="Destination Name")]
        public string Name { get; set; }
    }
}
