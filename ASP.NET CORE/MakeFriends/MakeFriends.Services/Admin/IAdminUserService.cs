namespace MakeFriends.Services.Admin
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserListingServiceModel>> AllUsers(int page = 1);

        Task<IEnumerable<AdminUserListingServiceModel>> Moderators();

        Task<IEnumerable<AdminUserListingServiceModel>> Administrators();

        int TotalPagesWithUsers();

        IQueryable<AdminUserInfoServiceModel> GetUser(string userId);

        Task<bool> UserPhotosUpdateStatus(string userId, List<UserImages> photos);

        Task<bool> DeleteUserAsync(string userId);

        Task<bool> DeleteUserRoleAsync(string userId);

        IEnumerable<string> GetAllRoles();

        Task<bool> AddUserToRole(string userId, string role);
    }
}
