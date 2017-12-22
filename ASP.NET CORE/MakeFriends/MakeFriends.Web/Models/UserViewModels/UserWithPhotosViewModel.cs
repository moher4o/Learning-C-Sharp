using System.Collections.Generic;
using MakeFriends.Common.Mapping;
using MakeFriends.Services.Models;

namespace MakeFriends.Web.Models.UserViewModels 
{
    public class UserWithPhotosViewModel : IMapFrom<UserWithPhotoCollectionServiceModel>
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public IEnumerable<string> Photos { get; set; }

    }
}
