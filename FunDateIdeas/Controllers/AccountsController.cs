using FunDateIdeas.Data.Identity;
using FunDateIdeas.ViewModels;
using FunDateIdeas.ViewModels.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDateIdeas.Controllers
{
    [AllowAnonymous]
    public class AccountsController : Controller
    {
        private readonly SignInManager<FunDateIdeasUser> _signInManager;
        private readonly UserManager<FunDateIdeasUser> _userManager;

        public AccountsController(UserManager<FunDateIdeasUser> userManager, SignInManager<FunDateIdeasUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return RedirectToAction("SignIn");
        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignInAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(username, password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            TempData["SignInError"] = "Unable to validate your credentials";
            return RedirectToAction("SignIn");
        }

        public IActionResult ManageUsers()
        {
            var users = _userManager.Users;
            List<ManageUsersViewModel> model = new List<ManageUsersViewModel>();
            if (users.Count() > 0)
            {
                foreach (var user in users)
                {
                    model.Add(new ManageUsersViewModel() { Id = user.Id, UserName = user.UserName });
                }
            }
            return View(model);
        }

        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(string username, string password)
        {
            var result = await _userManager.CreateAsync(new FunDateIdeasUser() { UserName = username }, password);
            if (result.Succeeded)
            {
                return RedirectToAction("ManageUsers");
            }
            else
            {
                ViewBag.Message = "Failed to create user account. This User may be in the system or the input data may be invalid";
                return View();
            }
        }

        public async Task<IActionResult> UpdateUserAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var model = new UpdateUserViewModel() { Id = user.Id, UserName = user.UserName };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserViewModel userModel)
        {
            var user = await _userManager.FindByIdAsync(userModel.Id);
            user.UserName = userModel.UserName;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("UpdateUser", new { Id = user.Id });
            }
            else
            {
                ViewBag.Message = "There was a problem updating this user's information. Please check your inputs and try again";
                return View(user);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUserAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("ManageUsers");
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Account");
        }
    }
}
