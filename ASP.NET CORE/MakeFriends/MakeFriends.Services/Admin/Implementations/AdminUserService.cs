using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MakeFriends.Services.Admin.Models;
using MakeFriends.Data;
using System.Linq;
using static MakeFriends.Data.DataConstants;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using MakeFriends.Data.Models;

namespace MakeFriends.Services.Admin.Implementations
{
    public class AdminUserService : IAdminUserService
    {
        private readonly FriendsDbContext db;
        private readonly IUploadFile files;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public AdminUserService(FriendsDbContext db, IUploadFile files, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.db = db;
            this.files = files;
            this.roleManager = roleManager;
            this.userManager = userManager;
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

            var visits = this.db.Visitors.Where(v => v.ObserverId == userToDelete.Id || v.VisitedUserId == userToDelete.Id).ToList();
            this.db.Visitors.RemoveRange(visits);

            var messages = this.db.Messages.Where(m => m.SenderId == userToDelete.Id || m.ReceiverId == userToDelete.Id).ToList();
            this.db.Messages.RemoveRange(messages);

            var favorites = this.db.Favorites.Where(f => f.UserId == userToDelete.Id || f.FavoriteUserId == userToDelete.Id).ToList();
            this.db.Favorites.RemoveRange(favorites);

            var images = this.db.Images.Where(i => i.UserId == userToDelete.Id).ToList();
            this.db.Images.RemoveRange(images);

            if (this.files.DeleteUserDirectory(userToDelete.Id))
            {
                this.db.Users.Remove(userToDelete);
                await this.db.SaveChangesAsync();
            }

            return true;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllUsers(int page = 1)
        {
            var users = this.db.Users.Where(u => u.RegistrationDate == null).ToList();
            foreach (var user in users)
            {
                user.RegistrationDate = DateTime.UtcNow;
            }
            var selectedUsers = this.db.Users
                .OrderByDescending(u => u.Images.Where(i => i.IsApproved == false).Count())
                .Skip((page - 1) * AdminUsersPerPageCount)
                .Take(AdminUsersPerPageCount)
                .ProjectTo<AdminUserListingServiceModel>()
                .ToList();
            foreach (var userInList in selectedUsers)
            {
                userInList.Roles = await this.userManager.GetRolesAsync(await this.userManager.FindByIdAsync(userInList.Id));
            }

            return selectedUsers;
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

        public int TotalPagesWithUsers()
        {
            return (int)Math.Ceiling(this.db.Users.Count() / (decimal)AdminUsersPerPageCount);
        }

        public IQueryable<AdminUserInfoServiceModel> GetUser(string userId)
        {
            if (userId == null)
            {
                return null;
            }

            var user = this.db.Users
                .Where(u => u.Id == userId)
                .ProjectTo<AdminUserInfoServiceModel>()
                .AsQueryable();


            return user;
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

        public async Task<bool> UserPhotosUpdateStatus(string userId, List<UserImages> photos)
        {
            if (userId == null || photos == null)
            {
                return false;
            }
            foreach (var photo in photos)
            {
                if (photo.IsApproved)
                {
                    var photoName = photo.PhotoPath.Substring(photo.PhotoPath.LastIndexOf('/') + 1);
                    var userPhoto = this.db.Images
                        .FirstOrDefault(i => i.UserId == userId && i.PhotoName == photoName);
                    userPhoto.IsApproved = photo.IsApproved;
                }
                else
                {
                    this.files.DeleteUserPhoto(photo.PhotoPath);
                }
            }
            await this.db.SaveChangesAsync();

            var images = this.db.Images.Where(i => i.UserId == userId && !i.IsApproved).ToList();
            if (images.Count() != 0)
            {
                this.db.Images.RemoveRange(images);
                await this.db.SaveChangesAsync();
            }

            return true;
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
    }
}
