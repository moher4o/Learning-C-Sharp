using AutoMapper.QueryableExtensions;
using MakeFriends.Data;
using MakeFriends.Data.Models;
using MakeFriends.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MakeFriends.Services.Implementations
{
    public class VisitorService : IVisitorService
    {
        private readonly FriendsDbContext db;

        public VisitorService(FriendsDbContext db)
        {
            this.db = db;
        }

        public void NewLike(string visitorId, string likedUserId)
        {
            if (visitorId == null || likedUserId == null)
            {
                return;
            }
            var liked = this.db.Visitors
                .Where(v => v.ObserverId == visitorId && v.VisitedUserId == likedUserId)
                .FirstOrDefault();

            if (liked != null)
            {
                liked.VisitDate = DateTime.UtcNow;
                this.db.SaveChanges();
                return;
            }

            var visit = new Visitor
            {
                ObserverId = visitorId,
                VisitedUserId = likedUserId,
                IsLiked = true,
                VisitDate = DateTime.UtcNow
            };
            this.db.Visitors.Add(visit);
            this.db.SaveChanges();

            return;
        }

        public IEnumerable<AdmirersServiceModel> GetUsersWhoLikeCurrentUser(string userId)
        {
            if (userId == null)
            {
                return null;
            }

            var admirers = this.db.Visitors
                .Where(v => v.VisitedUserId == userId && v.VisitDate > DateTime.UtcNow.AddMonths(-6) && v.IsLiked == true)
                .Select(v => v.Observer)
                .ProjectTo<AdmirersServiceModel>()
                .ToList();

            return admirers;
        }

        public IEnumerable<VisitorServiceModel> GetUsersWhoVisitCurrentUser(string visitedUserId)
        {
            if (visitedUserId == null)
            {
                return null;
            }

            var visitors = this.db.Visitors
                .Where(v => v.VisitedUserId == visitedUserId && v.VisitDate > DateTime.UtcNow.AddMonths(-6))
                .Select(v => v.Observer)
                .ProjectTo<VisitorServiceModel>(new { visitedUserId })
                .ToList();

            return visitors;
        }
    }
}
