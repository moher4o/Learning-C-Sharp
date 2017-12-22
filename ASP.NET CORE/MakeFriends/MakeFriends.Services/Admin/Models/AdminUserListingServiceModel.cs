using MakeFriends.Common.Mapping;
using MakeFriends.Data.Models;

namespace MakeFriends.Services.Admin.Models
{
    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Credits { get; set; }
    }
}
