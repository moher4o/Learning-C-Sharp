using MakeFriends.Data;
using MakeFriends.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;
using MakeFriends.Data.Models;
using System.Threading.Tasks;

namespace MakeFriends.Services.Implementations
{
   public class UserService : IUserService
    {
        private readonly FriendsDbContext db;

        public UserService(FriendsDbContext db)
        {
            this.db = db;
        }

        public async Task<IQueryable<FullUserInfoServiceModel>> GetUserInfoAsync(string visitedUserId, string observerId)
        {
            if (visitedUserId == null || observerId == null)
            {
                return null;
            }

            var visit = this.db.Visitors
                .Where(v => v.ObserverId == observerId && v.VisitedUserId == visitedUserId)
                .FirstOrDefault();
            if (visit != null)
            {
                visit.VisitDate = DateTime.UtcNow;
            }
            else
            {
                await this.db.Visitors.AddAsync(new Visitor
                {
                    ObserverId = observerId,
                    VisitedUserId = visitedUserId,
                    VisitDate = DateTime.UtcNow
                });

                await this.db.SaveChangesAsync();
            }

            var userInfo = this.db.Users
                .Where(u => u.Id == visitedUserId)
                .ProjectTo<FullUserInfoServiceModel>()
                .AsQueryable();

            return userInfo;
        }
    }
}
