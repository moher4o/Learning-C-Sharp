using System.ComponentModel.DataAnnotations;
using static MakeFriends.Data.DataConstants;


namespace MakeFriends.Data.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public string SenderId { get; set; }

        public User Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        public User Receiver { get; set; }

        [Required]
        [MinLength(MessageMinLength)]
        [MaxLength(MessageMaxLength)]
        public string Content { get; set; }

        public bool IsReceived { get; set; } = false;
    }
}
