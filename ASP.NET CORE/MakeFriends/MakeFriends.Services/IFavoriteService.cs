using MakeFriends.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriends.Services
{
    public interface IFavoriteService
    {
        Task<bool> Add(string userId, string favoriteUserId);

        IEnumerable<UserFavoritesServiceModel> GetUserFavorites(string userId);
    }
}
