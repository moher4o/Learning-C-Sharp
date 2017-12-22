using MakeFriends.Services.Models;
using System.Collections.Generic;


namespace MakeFriends.Services
{
    public interface IImageService
    {
        string AddImage(string userId);

        bool IsImagesLimitReached(string userId);

        IEnumerable<string> GetUserImagesPath(string userId);

        bool DeleteImage(string userId, string photoName);

        IEnumerable<UserWithPhotoCollectionServiceModel> GetRandomUsers();

        UserWithPhotoCollectionServiceModel GetUserWithImages(string userId);
    }
}
