using System.ComponentModel.DataAnnotations;
using static MakeFriends.Data.DataConstants;

namespace MakeFriends.Data.Models
{
    public class UserPhoto
    {

        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public User Individual { get; set; }

        [Required]
        [MaxLength(PictureNameMaxLength)]
        public string PhotoName { get; set; }

        public bool IsApproved { get; set; } = false;

        public bool IsPrivate { get; set; } = false;
    }
}
