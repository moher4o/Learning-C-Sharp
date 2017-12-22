using AutoMapper.QueryableExtensions;
using MakeFriends.Data;
using MakeFriends.Data.Models;
using MakeFriends.Services.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeFriends.Services.Implementations
{
    public class FavoriteService : IFavoriteService
    {
        private readonly FriendsDbContext db;
        private readonly IImageService images;
        

        public FavoriteService(FriendsDbContext db, IImageService images)
        {
            this.db = db;
            this.images = images;
        }

        public async Task<bool> Add(string userId, string favoriteUserId)
        {
            if (userId == null || favoriteUserId == null)
            {
                return false;
            }

            var user = await this.db.Users.FindAsync(userId);
            var favoriteUser = await this.db.Users.FindAsync(favoriteUserId);
            if (user == null || favoriteUser == null)
            {
                return false;
            }

            var favorite = new Favorite
            {
                UserId = userId,
                FavoriteUserId = favoriteUserId
            };

            if (await this.db.Favorites.FindAsync(userId, favoriteUserId) == null)
            {
                await this.db.Favorites.AddAsync(favorite);
                await this.db.SaveChangesAsync();
            }

            return true;
        }

        public IEnumerable<UserFavoritesServiceModel> GetUserFavorites(string userId)
        {
            if (userId == null)
            {
                return null;
            }

            var favorites = this.db.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => f.FavoriteUser)
                .ProjectTo<UserFavoritesServiceModel>()
                .ToList();

            return favorites;

        }

    }
}
