using MakeFriends.Services.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriends.Web.Areas.Admin.Models.Users
{
    public class UsersPagingViewModel
    {
        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? this.CurrentPage : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.CurrentPage : this.CurrentPage + 1;

        public int TotalPages { get; set; }
    }
}
