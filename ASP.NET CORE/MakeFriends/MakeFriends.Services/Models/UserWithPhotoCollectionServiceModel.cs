using System.Collections.Generic;

namespace MakeFriends.Services.Models
{
    public class UserWithPhotoCollectionServiceModel
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public IEnumerable<string> Photos { get; set; }
    }
}
