using BookTravel.Services.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookTravel.Services.Admin
{
    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserListingServiceModel>> Moderators();

        Task<IEnumerable<AdminUserListingServiceModel>> Administrators();

        Task<bool> DeleteUserAsync(string userId);

        Task<bool> DeleteUserRoleAsync(string userId);

        IEnumerable<string> GetAllRoles();

        Task<bool> AddUserToRole(string userId, string role);
    }
}
