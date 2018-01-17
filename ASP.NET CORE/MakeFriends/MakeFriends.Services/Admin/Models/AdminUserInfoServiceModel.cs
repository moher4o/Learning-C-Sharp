using AutoMapper;
using MakeFriends.Common.Mapping;
using MakeFriends.Data.Models;
using MakeFriends.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using static MakeFriends.Data.DataConstants;

namespace MakeFriends.Services.Admin.Models
{
   public class AdminUserInfoServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        [Required]
        public string Id { get; set; }

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

        public IEnumerable<UserImages> Photos { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<User, AdminUserInfoServiceModel>()
                   .ForMember(u => u.Photos, cfg => cfg.MapFrom(u => u.Images.OrderBy(i => i.IsApproved).Select(i => new UserImages {
                       PhotoPath = "/" + UserPhotoSubDirectory + "/" + u.Id + "/" + i.PhotoName,
                       IsApproved = i.IsApproved
                   }).ToList()));
        }
    }
}
