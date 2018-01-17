using MakeFriends.Services;
using MakeFriends.Web.Models.UserViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using MakeFriends.Data.Models;
using Microsoft.AspNetCore.Authorization;
using static MakeFriends.Data.DataConstants;
using MakeFriends.Services.Models;
using AutoMapper;
using MakeFriends.Web.Infrastructure.Filters;

namespace MakeFriends.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IImageService images;
        private readonly IVisitorService visitors;
        private readonly IFavoriteService favorities;
        private readonly IUserService users;
        private readonly IMessageService messages;

        public UserController(IImageService images,
            UserManager<User> userManager,
            IVisitorService visitors,
            IFavoriteService favorities,
            IUserService users,
            IMessageService messages)
        {
            this.userManager = userManager;
            this.images = images;
            this.visitors = visitors;
            this.favorities = favorities;
            this.users = users;
            this.messages = messages;
        }

        [Authorize]
        public async Task<IActionResult> NewFriends(string wantedUserId, string dislikedUserId)
        {
            var user = await userManager.GetUserAsync(User);

            var selectedUsers = await this.GetRandomUsers();

            if (dislikedUserId != null)
            {
                selectedUsers = selectedUsers.Where(u => u.UserId != dislikedUserId);
            }

            var newFriends = new NewFriendsViewModel()
            {
                Users = selectedUsers
                    .ProjectTo<UserWithPhotosViewModel>()
                    .ToList()
            };


            if (wantedUserId == null)
            {
                newFriends.SelectedUser = newFriends.Users.FirstOrDefault();
            }
            else
            {
                var userOnLights = this.images
                    .GetUserWithImages(wantedUserId);

                newFriends.SelectedUser = new UserWithPhotosViewModel()
                {
                    FirstName = userOnLights.FirstName,
                    UserId = userOnLights.UserId,
                    Photos = userOnLights.Photos
                };

            }

            return View(newFriends);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> NewFriends(string likedUserId)
        {
            if (likedUserId == null)
            {
                return BadRequest();
            }

            var user = await userManager.GetUserAsync(User);

            var selectedUsers = await this.GetRandomUsers();

            this.NewLike(user.Id, likedUserId);

            selectedUsers = selectedUsers.Where(u => u.UserId != likedUserId);


            var newFriends = new NewFriendsViewModel()
            {
                Users = selectedUsers
                        .ProjectTo<UserWithPhotosViewModel>()
                        .ToList()
            };
            newFriends.SelectedUser = newFriends.Users.FirstOrDefault();

            return View(newFriends);

        }

        public async Task<IActionResult> UserInfo(string userId)
        {
            if (userId == null)
            {
                return BadRequest();
            }

            var observer = await userManager.GetUserAsync(User);

            var serviceInfo = await this.users.GetUserInfoAsync(userId, observer.Id);

            var userInfo = serviceInfo                
                 .ProjectTo<FullUserInfoViewModel>()
                 .FirstOrDefault();

            userInfo.Photos = this.images.GetUserImagesPath(userId);

            return View(userInfo);
        }

        
        public async Task<IActionResult> Favorites()
        {
            var user = await userManager.GetUserAsync(User);

            var favoritesUsers = this.favorities.GetUserFavorites(user.Id);

            var result = favoritesUsers.AsQueryable()
               .ProjectTo<FavoritiesViewModel>()
               .ToList();

            return View(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Favorites(string favoriteUserId)
        {
            if (favoriteUserId == null)
            {
                return BadRequest();
            }

            var user = await userManager.GetUserAsync(User);

            await this.favorities.Add(user.Id, favoriteUserId);

            var favoritesUsers = this.favorities.GetUserFavorites(user.Id);

             return View(favoritesUsers.AsQueryable()
                .ProjectTo<FavoritiesViewModel>()
                .ToList());
        }

        [Authorize]
        public async Task<IActionResult> Likes()
        {
            var user = await userManager.GetUserAsync(User);

            var admirers = this.visitors.GetUsersWhoLikeCurrentUser(user.Id)
                .AsQueryable()
                .ProjectTo<AdmirersViewModel>()
                .ToList();

            return View(admirers);
        }

        [Authorize]
        public async Task<IActionResult> Visitors()
        {
            var user = await userManager.GetUserAsync(User);

            var userVisitors = this.visitors.GetUsersWhoVisitCurrentUser(user.Id)
                .AsQueryable()
                .ProjectTo<VisitorViewModel>()
                .ToList();

            return View(userVisitors);
        }

        public async Task<IActionResult> Messages(string receiverId)
        {
            if (receiverId == null)
            {
                return BadRequest();
            }

            var userSender = await this.userManager.GetUserAsync(User);
            var userReceiver = await this.userManager.FindByIdAsync(receiverId);

            if (userReceiver == null)
            {
                return BadRequest();
            }

            var conversation = this.messages.GetMessages(userSender.Id, userReceiver.Id);

            return View(new MessageViewModel {
                Messages = conversation,
                SenderId = userSender.Id,
                ReceiverId = userReceiver.Id,
                ReceiverFirstName = userReceiver.FirstName
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Messages(MessageViewModel model)
        {
            var userReceiver = await this.userManager.FindByIdAsync(model.ReceiverId);

            if (!ModelState.IsValid)
            {
                model.Messages = this.messages.GetMessages(model.SenderId, model.ReceiverId);
                model.ReceiverFirstName = userReceiver.FirstName;
                return View(model);
            }

            if(await this.messages.AddNewMessage(model.SenderId, model.ReceiverId, model.Content))
            {
                TempData[SuccessMessageKey] = "Message send";
                model.Messages = this.messages.GetMessages(model.SenderId, model.ReceiverId);
                model.ReceiverFirstName = userReceiver.FirstName;
                model.Content = null;
                return View(model);
            }
            else
            {
                TempData[ErrorMessageKey] = "Message not send";
                model.Messages = this.messages.GetMessages(model.SenderId, model.ReceiverId);
                model.ReceiverFirstName = userReceiver.FirstName;
                return View(model);
            }
        }

        private void NewLike(string visitorId, string likedUserId)
        => this.visitors.NewLike(visitorId, likedUserId);

        private async Task<IQueryable<UserWithPhotoCollectionServiceModel>> GetRandomUsers()
        {
            var user = await userManager.GetUserAsync(User);
            return this.images
                .GetRandomUsers()
                .AsQueryable()
                .Where(u => u.UserId != user.Id);

        }

    }
}
