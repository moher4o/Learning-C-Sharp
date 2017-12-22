using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MakeFriends.Data.Models
{
   public class Visitor
    {
        [Required]
        public string VisitedUserId { get; set; }

        public User VisitedUser { get; set; }

        [Required]
        public string ObserverId { get; set; }

        public User Observer { get; set; }

        public bool IsLiked { get; set; } = false;

        public DateTime VisitDate { get; set; }

        public bool IsSeen { get; set; } = false;
    }
}
