using MakeFriends.Services.Models;
using System.Collections.Generic;

namespace MakeFriends.Services
{
    public interface IVisitorService
    {
        void NewLike(string visitorId, string LikedUserId);

        IEnumerable<AdmirersServiceModel> GetUsersWhoLikeCurrentUser(string userId);

        IEnumerable<VisitorServiceModel> GetUsersWhoVisitCurrentUser(string visitedUserId);
    }
}
