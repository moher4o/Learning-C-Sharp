using MakeFriends.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static MakeFriends.Data.DataConstants;

namespace MakeFriends.Web.Models.ManageViewModels
{
    public class PersonalDataViewModel
    {
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Range(0, 250, ErrorMessage = "Height can be between 0 and 250")]
        public int? Height { get; set; }

        [Range(0, 250, ErrorMessage = "Weight can be between 0 and 250")]
        public int? Weight { get; set; }

        [Display(Name = "Eyes Color")]
        public EyesColor? EyesColor { get; set; }

        public HairColor? HairColor { get; set; }

        public bool? IsSmoking { get; set; }

        public bool? IsDrinking { get; set; }

        public Sexuality? Sexuality { get; set; }

        public Gender? Gender { get; set; }

        public Relations? Relations { get; set; }
    }
}
