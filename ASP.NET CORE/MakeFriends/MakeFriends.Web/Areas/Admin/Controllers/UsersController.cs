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

namespace MakeFriends.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DataConstants.AdministratorRole)]
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
            var userpage = this.users.AllUsers(page);


            return View(new UsersPagingViewModel {
                CurrentPage = page,
                Users = userpage,
                TotalPages = this.users.TotalPagesWithUsers()
            });
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
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(AllUsers), new { page = 1 });
            }


            return View(model);

        }
    }
}
