using AutoMapper;
using MakeFriends.Common.Mapping;
using MakeFriends.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace MakeFriends.Services.Admin.Models
{
    public class AdminUserListingServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Credits { get; set; }

        public int NotApprovedPhotoCount { get; set; }

        public ICollection<string> Roles { get; set; } = new HashSet<string>();

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<User, AdminUserListingServiceModel>()
                   .ForMember(u => u.NotApprovedPhotoCount, cfg => cfg.MapFrom(i => i.Images.Where(im => im.IsApproved == false).Count()));
        }
    }
}
