using System.Collections.Generic;

namespace MakeFriends.Web.Models.UserViewModels
{
    public class NewFriendsViewModel
    {
        public IEnumerable<UserWithPhotosViewModel> Users { get; set; }

        public UserWithPhotosViewModel SelectedUser { get; set; }
    }
}
