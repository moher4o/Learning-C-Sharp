using MakeFriends.Data;
using MakeFriends.Data.Models;
using MakeFriends.Services.Admin;
using MakeFriends.Web.Areas.Admin.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using static MakeFriends.Data.DataConstants;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MakeFriends.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator, Moderator")]
    public class UsersController : Controller
    {
        private readonly IAdminUserService users;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IAdminUserService users)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.users = users;
        }

        public async Task<IActionResult> AllUsers(int page = 1)
        {
            var userpage = await this.users.AllUsers(page);

            return View(new UsersPagingViewModel {
                CurrentPage = page,
                Users = userpage,
                TotalPages = this.users.TotalPagesWithUsers()
            });
        }

        public IActionResult RemoveRole(string userId)
        {
            return View(new RemoveRoleViewModel() {
                UserId = userId
            });
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRole)]
        public async Task<IActionResult> DestroyUserRole(string userId)
        {

            if (userId == null)
            {
                TempData[ErrorMessageKey] = "Invalid User";
                return RedirectToAction(nameof(AllUsers), new { page = 1 });
            }

            if (await this.users.DeleteUserRoleAsync(userId))
            {
                TempData[SuccessMessageKey] = "All Roles removed";
            }
            else
            {
                TempData[ErrorMessageKey] = "Roles not remomed";
            }

            return RedirectToAction(nameof(AllUsers), new { page = 1 });
        }

        [Authorize(Roles = AdministratorRole)]
        public async Task<IActionResult> Moderators()
        {
            return View(await this.users.Moderators());
        }

        [Authorize(Roles = AdministratorRole)]
        public async Task<IActionResult> Administrators()
        {
            return View(await this.users.Administrators());
        }


        public IActionResult DeleteUser(string userId, string email)
        {
            if (userId == null)
            {
                TempData[ErrorMessageKey] = "Invalid User";
                return RedirectToAction(nameof(AllUsers), new { page = 1 });
            }



            return View(new DeleteUserViewModel
            {
                UserId = userId,
                Email = email
            });
        }

        public async Task<IActionResult> DestroyUser(string userId)
        {
            if (!await ValidateAdminOrModeratorRights())
            {
                return RedirectToAction("Index", "Home");
            }

            if (userId == null)
            {
                TempData[ErrorMessageKey] = "Invalid User";
                return RedirectToAction(nameof(AllUsers), new { page = 1 });
            }

            if (await this.users.DeleteUserAsync(userId))
            {
                TempData[SuccessMessageKey] = "User deleted";
            }
            
            return RedirectToAction(nameof(AllUsers), new { page = 1 });
        }

        public async Task<IActionResult> EditUser(string userId)
        {
            if (userId == null)
            {
                return RedirectToAction(nameof(AllUsers), new { page = 1});
            }

            var user = await this.userManager.FindByIdAsync(userId);

            var userInfo = this.users.GetUser(user.Id)
                .ProjectTo<AdminUserInfoViewModel>()
                .FirstOrDefault();

            return View(userInfo);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(AdminUserInfoViewModel model)
        {
            if (!await ValidateAdminOrModeratorRights())
            {
                return RedirectToAction("Index", "Home");
            }

            if (model.Id == null || model.Photos == null)
            {
                TempData[ErrorMessageKey] = "Invalid data";
                return RedirectToAction(nameof(AllUsers), new { page = 1 });
            }

            var result = await this.users.UserPhotosUpdateStatus(model.Id, model.Photos);
            if (result)
            {
                TempData[SuccessMessageKey] = "Updated status of user photos";
            }
            
            return RedirectToAction(nameof(this.EditUser),new { userId = model.Id});

        }

        public IActionResult AddToRole(string userId)
        {

            var roles = this.users.GetAllRoles();

            var result = new AddToRoleViewModel()
            {
                UserId = userId,
                Roles = roles.Where(r => r != AdministratorRole)
                .Select(r => new SelectListItem
                {
                    Text = r,
                    Value = r
                }).ToList()
            };

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(string userId, string role)
        {
            if (!await ValidateAdminOrModeratorRights())
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                TempData[ErrorMessageKey] = "Invalid operation";
                return RedirectToAction(nameof(AllUsers));
            }

            bool result = await this.users.AddUserToRole(userId, role);

            if (result)
            {
                TempData[SuccessMessageKey] = $"Role {role} added to the user";
            }
            else
            {
                TempData[ErrorMessageKey] = $"Role {role} can't be added to the user";
            }

            return RedirectToAction(nameof(AllUsers));
        }

        private async Task<bool> ValidateAdminOrModeratorRights()
        {
            var currentUser = await this.userManager.GetUserAsync(User);

            var adminRights = await this.userManager.IsInRoleAsync(currentUser, AdministratorRole) || await this.userManager.IsInRoleAsync(currentUser, ModeratorRole);
            if (!adminRights)
            {
                TempData[ErrorMessageKey] = "Insufficient rights!";
                return false;
            }
            return true;
        }

    }
}
