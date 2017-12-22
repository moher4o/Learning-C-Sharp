using MakeFriends.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriends.Web.Models.UserViewModels
{
    public class MessageViewModel
    {
        public IEnumerable<MessagesServiceModel> Messages { get; set; }

        [Required]
        public string SenderId { get; set; }

        public string SenderFirstName { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        public string ReceiverFirstName { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        [Display(Name = "Message")]
        public string Content { get; set; }
    }
}
