using MakeFriends.Common.Mapping;
using MakeFriends.Data.Models.Enums;
using MakeFriends.Services.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static MakeFriends.Data.DataConstants;

namespace MakeFriends.Web.Areas.Admin.Models.Users
{
    public class AdminUserInfoViewModel : IMapFrom<AdminUserInfoServiceModel>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MinLength(UserNameMinLength)]
        [MaxLength(UserNameMaxLength)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(UserNameMinLength)]
        [MaxLength(UserNameMaxLength)]
        [Display(Name = "Last Name")]
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

        [Display(Name = "Eyes Color")]
        public EyesColor? EyesColor { get; set; }

        [Display(Name = "Hair Color")]
        public HairColor? HairColor { get; set; }

        [Display(Name = "Smoker")]
        public bool? IsSmoking { get; set; }

        [Display(Name = "Drinker")]
        public bool? IsDrinking { get; set; }

       
        public Sexuality? Sexuality { get; set; }

        public Gender? Gender { get; set; }

        public Relations? Relations { get; set; }

        public List<UserImages> Photos { get; set; }

    }
}
