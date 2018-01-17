using BookTravel.Data.Models;
using BookTravel.Services.Admin;
using BookTravel.Web.Areas.Admin.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookTravel.Data.DataConstants;

namespace BookTravel.Web.Areas.Admin.Controllers
{
    public class UsersController : BaseController
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

        public IActionResult RemoveRole(string userId)
        {
            return View(new RemoveRoleViewModel()
            {
                UserId = userId
            });
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
                return RedirectToAction(nameof(Moderators));
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

            return RedirectToAction(nameof(Moderators));
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRole)]
        public async Task<IActionResult> DestroyUserRole(string userId)
        {

            if (userId == null)
            {
                TempData[ErrorMessageKey] = "Invalid User";
                return RedirectToAction(nameof(Moderators));
            }

            if (await this.users.DeleteUserRoleAsync(userId))
            {
                TempData[SuccessMessageKey] = "All Roles removed";
            }
            else
            {
                TempData[ErrorMessageKey] = "Roles not remomed";
            }

            return RedirectToAction(nameof(Moderators));
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
                return RedirectToAction(nameof(Moderators));
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
                return RedirectToAction(nameof(Moderators));
            }

            if (await this.users.DeleteUserAsync(userId))
            {
                TempData[SuccessMessageKey] = "User deleted";
            }

            return RedirectToAction(nameof(Moderators));
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

        public IActionResult CreateNewUser()
        {
            return View(new CreateUserViewModel());
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRole)]
        public async Task<IActionResult> CreateNewUser(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

           var newUser = new User
            {
                Email = model.Email,
                UserName = model.Email,
            };
            await userManager.CreateAsync(newUser, model.Password);

            await userManager.AddToRoleAsync(newUser, ModeratorRole);

            TempData[SuccessMessageKey] = $"User {model.Email} created and asigned to role {ModeratorRole}";

            return RedirectToAction(nameof(Moderators));
        }

        public IActionResult CreateNewAdmin()
        {
            return View(new CreateUserViewModel());
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRole)]
        public async Task<IActionResult> CreateNewAdmin(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var newUser = new User
            {
                Email = model.Email,
                UserName = model.Email,
            };
            await userManager.CreateAsync(newUser, model.Password);

            await userManager.AddToRoleAsync(newUser, AdministratorRole);

            TempData[SuccessMessageKey] = $"User {model.Email} created and asigned to role {AdministratorRole}";

            return RedirectToAction(nameof(Administrators));
        }

    }
}
