using Microsoft.AspNetCore.Identity;
using MakeFriends.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static MakeFriends.Data.DataConstants;
using System.Collections.Generic;
using System;

namespace MakeFriends.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser
    {
        [Required]
        [MinLength(UserNameMinLength)]
        [MaxLength(UserNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(UserNameMinLength)]
        [MaxLength(UserNameMaxLength)]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int Credits { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Range(0, 250, ErrorMessage = "Height can be between 0 and 250")]
        public int? Height { get; set; }

        [Range(0, 250, ErrorMessage = "Weight can be between 0 and 250")]
        public int? Weight { get; set; }

        public EyesColor? EyesColor { get; set; }

        public HairColor? HairColor { get; set; }

        public bool? IsSmoking { get; set; }

        public bool? IsDrinking { get; set; }

        public Sexuality? Sexuality { get; set; }

        public Gender? Gender { get; set; }

        public Relations? Relations { get; set; }

        public ICollection<Message> SendMessages { get; set; } = new List<Message>();

        public ICollection<Message> ReceivedMesages { get; set; } = new List<Message>();

        public ICollection<UserPhoto> Images { get; set; } = new List<UserPhoto>();

        public ICollection<Visitor> Visits { get; set; } = new List<Visitor>();

        public ICollection<Visitor> Observed { get; set; } = new List<Visitor>();

        public ICollection<Favorite> FavoriteUsers { get; set; } = new List<Favorite>();
    }
}
