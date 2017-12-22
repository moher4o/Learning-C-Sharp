using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MakeFriends.Services.Admin.Models;
using MakeFriends.Data;
using System.Linq;
using static MakeFriends.Data.DataConstants;
using AutoMapper.QueryableExtensions;

namespace MakeFriends.Services.Admin.Implementations
{
    public class AdminUserService : IAdminUserService
    {
        private readonly FriendsDbContext db;

        public AdminUserService(FriendsDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<AdminUserListingServiceModel> AllUsers(int page = 1)
        {
            var users = this.db.Users.Where(u => u.RegistrationDate == null).ToList();
            foreach (var user in users)
            {
                user.RegistrationDate = DateTime.UtcNow;
            }
            return this.db.Users
                .OrderByDescending(u => u.RegistrationDate)
                .Skip((page - 1) * AdminUsersPerPageCount)
                .Take(AdminUsersPerPageCount)
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
            throw new NotImplementedException();
        }

        public Task<bool> UserAddToRole(string userId, string role)
        {
            throw new NotImplementedException();
        }

    }
}
