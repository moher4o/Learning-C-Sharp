using BookTravel.Common.Mapping;
using BookTravel.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookTravel.Services.Admin.Models
{
    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public ICollection<string> Roles { get; set; } = new HashSet<string>();
    }
}
