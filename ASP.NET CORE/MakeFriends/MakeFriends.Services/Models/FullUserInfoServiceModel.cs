using AutoMapper;
using MakeFriends.Common.Mapping;
using MakeFriends.Data.Models;
using MakeFriends.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MakeFriends.Services.Models
{
   public class FullUserInfoServiceModel :IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Description { get; set; }

        public string ShortView { get; set; }

        public EyesColor? EyesColor { get; set; }

        public HairColor? HairColor { get; set; }

        public bool? IsSmoking { get; set; }

        public bool? IsDrinking { get; set; }

        public Sexuality? Sexuality { get; set; }

        public Relations? Relations { get; set; }

        public IEnumerable<string> Photos { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<User, FullUserInfoServiceModel>()
                .ForMember(u => u.ShortView, cfg => cfg.MapFrom(u => u.Height.ToString() + "sm, " + u.Weight.ToString() + "kg."))
                .ForMember(u => u.Name, cfg => cfg.MapFrom(u => u.FirstName + " " + u.LastName));
        }

    }
}
