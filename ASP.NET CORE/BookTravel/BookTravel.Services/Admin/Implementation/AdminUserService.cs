using AutoMapper.QueryableExtensions;
using BookTravel.Data;
using BookTravel.Data.Models;
using BookTravel.Services.Admin.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookTravel.Data.DataConstants;

namespace BookTravel.Services.Admin.Implementation
{
    public class AdminUserService : IAdminUserService
    {
        private readonly TravelDbContext db;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public AdminUserService(TravelDbContext db, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.db = db;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> Administrators()
        {
            var admins = await this.userManager.GetUsersInRoleAsync(AdministratorRole);

            return admins.AsQueryable()
                    .ProjectTo<AdminUserListingServiceModel>()
                    .ToList();

        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> Moderators()
        {
            var moderators = await this.userManager.GetUsersInRoleAsync(ModeratorRole);

            return moderators.AsQueryable()
                    .ProjectTo<AdminUserListingServiceModel>()
                    .ToList();

        }

        public IEnumerable<string> GetAllRoles()
        {
            return this.roleManager.Roles.Select(r => r.Name).ToList();
        }

        public async Task<bool> AddUserToRole(string userId, string role)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            var roleExists = await roleManager.RoleExistsAsync(role);
            if (user == null || !roleExists)
            {
                return false;
            }

            var result = await this.userManager.AddToRoleAsync(user, role);

            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserRoleAsync(string userId)
        {
            if (userId == null)
            {
                return false;
            }

            var user = await this.userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var roles = new List<string> { AdministratorRole, ModeratorRole };

            foreach (var role in roles)
            {
                await this.userManager.RemoveFromRoleAsync(user, role);
            }
            return true;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            if (userId == null)
            {
                return false;
            }

            var userToDelete = this.db.Users.FirstOrDefault(u => u.Id == userId);
            if (userToDelete == null)
            {
                return false;
            }

            this.db.Users.Remove(userToDelete);
            await this.db.SaveChangesAsync();

            return true;
        }

    }
}
