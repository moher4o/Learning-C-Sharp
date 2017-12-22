namespace MakeFriends.Services.Admin
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IAdminUserService
    {
        IEnumerable<AdminUserListingServiceModel> AllUsers(int page = 1);

        int TotalPagesWithUsers();

        IQueryable<AdminUserInfoServiceModel> GetUser(string userId);

        Task<bool> UserPhotosUpdateStatus(string userId, List<UserImages> photos);

        //IEnumerable<string> GetAllRoles();

        //Task<bool> UserAddToRole(string userId, string role);
    }
}
