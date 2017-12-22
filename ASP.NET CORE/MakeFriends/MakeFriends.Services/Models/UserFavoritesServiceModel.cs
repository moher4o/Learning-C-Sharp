using AutoMapper;
using MakeFriends.Common.Mapping;
using MakeFriends.Data;
using MakeFriends.Data.Models;
using System.Linq;

namespace MakeFriends.Services.Models
{
    public class UserFavoritesServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string PhotoPath { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<User, UserFavoritesServiceModel>()
                   .ForMember(u => u.PhotoPath, cfg => cfg.MapFrom(u => u.Images.Select(i => "/" + DataConstants.UserPhotoSubDirectory + "/" + u.Id + "/" + i.PhotoName).FirstOrDefault()));

        }
    }
}
