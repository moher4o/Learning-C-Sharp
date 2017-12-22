using MakeFriends.Data;
using MakeFriends.Data.Models;
using MakeFriends.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MakeFriends.Data.DataConstants;

namespace MakeFriends.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly FriendsDbContext db;

        public ImageService(FriendsDbContext db)
        {
            this.db = db;
        }

        public string AddImage(string userId)
        {
            if (userId == null)
            {
                return null;
            }

            string imageName;

            var lastUserImage = this.db.Images
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.Id)
                .Select(p => p.PhotoName)
                .FirstOrDefault();

            if (lastUserImage == null)
            {
                imageName = "1.jpg";
            }
            else
            {
                try
                {
                    imageName = (int.Parse(lastUserImage.Split('.')[0]) + 1).ToString() + ".jpg";
                }
                catch (Exception)
                {
                    return null;
                }
            }

            var image = new UserPhoto
            {
                UserId = userId,
                PhotoName = imageName,
            };
            this.db.Images.Add(image);
            this.db.SaveChanges();

            return imageName;
        }

        public bool DeleteImage(string userId, string photoName)
        {

            var image = this.db.Images
                .Where(i => i.UserId == userId && i.PhotoName == photoName)
                .FirstOrDefault();

            if (image!=null)
            {
                this.db.Images.Remove(image);
                this.db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<string> GetUserImagesPath(string userId)
        {
            
            string userDirectoryPath = "/" + UserPhotoSubDirectory + "/" + userId + "/";

            return this.db
               .Images
               .Where(i => i.UserId == userId)
               .Select(i => userDirectoryPath + i.PhotoName)
               .ToList();
        }

        public bool IsImagesLimitReached(string userId)
        {
            var userImages = this.db
                .Images
                .Where(i => i.UserId == userId)
                .ToList();

            return userImages.Count >= UserPhotoLimit;

        }

        public IEnumerable<UserWithPhotoCollectionServiceModel> GetRandomUsers()
        {
            var userCount = this.db.Users.Count();

            var randomList = GetRandomListOfInt(RandomUserToView < userCount ? RandomUserToView : userCount);

            var selectedUsers = new List<UserWithPhotoCollectionServiceModel>();
            foreach (var number in randomList)
            {
                var user = this.db.Users
                    .OrderBy(u => u.Id)
                    .Skip(number)
                    .Take(1)
                    .FirstOrDefault();
                var userWithPhotos = new UserWithPhotoCollectionServiceModel
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    Photos = this.GetUserImagesPath(user.Id)
                };

                selectedUsers.Add(userWithPhotos);
            }

            return selectedUsers;
        }

        public UserWithPhotoCollectionServiceModel GetUserWithImages(string userId)
        {
            if (userId == null)
            {
                return null;
            }
            var user = this.db.Users
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return null;
            }
            var userWithPhotos = new UserWithPhotoCollectionServiceModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                Photos = this.GetUserImagesPath(user.Id)
            };

           return userWithPhotos;
        }


        private IEnumerable<int> GetRandomListOfInt(int count)
        {

            int max = count != 0 ? this.db.Users.Count() : 0;
            int min = 0;
            var randomList = new List<int>(count);
            var r = new Random();
            var counter = 0;

            while (counter <= (count-1 > -1 ? count-1 : 0))
            {
                int currentNumber = r.Next(min, max);
                if (!randomList.Contains(currentNumber))
                {
                    randomList.Add(currentNumber);
                    counter++;
                }
            }

            return randomList;
        }
    }
}
