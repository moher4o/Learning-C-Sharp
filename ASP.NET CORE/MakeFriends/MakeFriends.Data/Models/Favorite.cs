using System.ComponentModel.DataAnnotations;

namespace MakeFriends.Data.Models
{
    public class Favorite
    {
        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public string FavoriteUserId { get; set; }

        public User FavoriteUser { get; set; }
    }
}
