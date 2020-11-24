using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Canoe.Data;
using Canoe.Models;
using Microsoft.AspNetCore.Identity;

namespace Canoe.Controllers
{

    
    public class ManageUsersController : Controller
    {
        
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        //**********************************************************************************************************
        // Constructor
        //
        // Set up the Controller and initalize modual level variables,
        //
        // Inputs:
        // userManager      reference to the Identity UserManager object
        //            
        //**********************************************************************************************************
        public ManageUsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        //**********************************************************************************************************
        // List
        //
        // 
        //
        // Inputs:
        // string id         id of the User
        //**********************************************************************************************************
        [HttpGet]
        public async Task<IActionResult> List(string searchString, int pageNumber = 1)
        {
            int pageSize = 10;

            var users = userManager.Users;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.UserName.Contains(searchString));
            }
            
            return View( await PaginatedList<ApplicationUser>.CreateAsync(users, pageNumber, pageSize, searchString));

        }
        //**********************************************************************************************************
        // ManageUserRoles
        //
        // 
        //
        // Inputs:
        // string id         id of the User
        //**********************************************************************************************************
        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userID = userId;
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found.";
                return View("NotFound");
            }

            var userRoles = new List<UserRoles>();

            foreach (var role in roleManager.Roles)
            {
                var userRole = new UserRoles
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRole.IsSelected = true;
                }
                else
                {
                    userRole.IsSelected = false;
                }

                userRoles.Add(userRole);

            }

            return View(userRoles);
        }

        //**********************************************************************************************************
        // ManageUserRoles
        //
        // 
        //
        // Inputs:
        // string id         id of the User
        //**********************************************************************************************************
        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRoles> userRoles, string userId)
        {
            
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found.";
                return View("NotFound");
            }

            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles.");
                return View(userRoles);
            }

            result = await userManager.AddToRolesAsync(user, userRoles.Where (x => x.IsSelected).Select (y=> y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user.");
                return View(userRoles);
            }

            return RedirectToAction("EditUser", new { Id = userId });
        }


        //**********************************************************************************************************
        // EditUser
        //
        // 
        //
        // Inputs:
        // string id         id of the User
        //**********************************************************************************************************
        [HttpGet]
        public async Task<IActionResult> EditUser(string id )
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with Id = {id} cannot be found.";
                    return View("NotFound");
                }

                IList<string> IuserRoles = await userManager.GetRolesAsync(user);
                List<string> userRoles = new List<string>(IuserRoles.Select(x => (string)x));

                var editUser = new EditUser
                {
                    Id = user.Id,
                    Email = user.Email,
                    AccessFailedCount = user.AccessFailedCount,
                    EmailConfirmed = user.EmailConfirmed,
                    LockoutEnabled = user.LockoutEnabled,
                    LockoutEnd = user.LockoutEnd.ToString(),
                    Roles = userRoles
                };
                return View(editUser);
            }
            catch
            {
                return View();
            }
        }

        //**********************************************************************************************************
        // EditUser
        //
        // 
        //
        // Inputs:
        // string id         id of the User
        //**********************************************************************************************************
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUser editUser)
        {
            try
            {
                //Find the user record from the Identity Table
                var user = await userManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with Id = {editUser.Id} cannot be found.";
                    return View("NotFound");
                }
                else
                { //Populate the User Object with the updates from the form
                    user.Email = editUser.Email;
                    user.AccessFailedCount = editUser.AccessFailedCount;
                    user.EmailConfirmed = editUser.EmailConfirmed;
                    user.LockoutEnabled = editUser.LockoutEnabled;

                    var result = await userManager.UpdateAsync(user);

                    //if it worked return to the list
                    if (result.Succeeded)
                    {
                        return RedirectToAction("List");
                    }
                    //else display the errors
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(editUser);
                }

            }
            catch
            {
                return View();
            }
        }


    }
}