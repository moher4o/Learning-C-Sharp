using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MakeFriends.Web.Areas.Admin.Models.Users
{
    public class AddToRoleViewModel
    {
        public IEnumerable<SelectListItem> Roles { get; set; }

        public string UserId { get; set; }
    }
}
