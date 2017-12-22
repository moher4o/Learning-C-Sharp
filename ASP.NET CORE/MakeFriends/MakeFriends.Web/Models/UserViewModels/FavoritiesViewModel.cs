using MakeFriends.Common.Mapping;
using MakeFriends.Services.Models;

namespace MakeFriends.Web.Models.UserViewModels
{
    public class FavoritiesViewModel : UserListViewModel, IMapFrom<UserFavoritesServiceModel>
    {
    }
}
