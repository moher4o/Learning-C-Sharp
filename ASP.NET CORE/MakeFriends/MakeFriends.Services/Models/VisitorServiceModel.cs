using AutoMapper;
using MakeFriends.Common.Mapping;
using MakeFriends.Data;
using MakeFriends.Data.Models;
using System;
using System.Linq;

namespace MakeFriends.Services.Models
{
    public class VisitorServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string PhotoPath { get; set; }

        public DateTime VisitDate { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            string visitedUserId = null;
            profile.CreateMap<User, VisitorServiceModel>()
                   .ForMember(u => u.PhotoPath, cfg => cfg.MapFrom(u => u.Images.Select(i => "/" + DataConstants.UserPhotoSubDirectory + "/" + u.Id + "/" + i.PhotoName).FirstOrDefault()))
                    .ForMember(u => u.VisitDate, cfg => cfg.MapFrom(u => u.Observed.Where(v => v.VisitedUserId == visitedUserId).Select(v => v.VisitDate).FirstOrDefault()));
        }
    }
}
