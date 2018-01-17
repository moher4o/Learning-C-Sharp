using System.ComponentModel.DataAnnotations;

namespace BookTravel.Web.Areas.Admin.Models
{
    public class NewAdminViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
