using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Auth;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using BusinessLogic.Models;
using Presentation.Models;
using Presentation.Utils.Services;

namespace Presentation.Controllers
{
    public class AuthController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMailService mailService;

        public AuthController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            IMailService mailService
        )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mailService = mailService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User() 
                { 
                    UserName = model.Email,
                    Email = model.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    string confirmationLink = 
                        Url.Action("ConfirmEmail", "Auth", 
                            new { userId = user.Id, token = token }, Request.Scheme);

                    //send confirmationLink
                    await mailService.SendEmailAsync(
                        new MailRequest { 
                            Subject = "Email Confirmation", 
                            ToEmail = model.Email, 
                            Body = $"Click on <a href='{confirmationLink}'>this link</a> to confirm your account." });

                    string errorTitle = "Registration successful";
                    string errorMessage = 
                        $"Please confirm your email by clicking on the link we sent you at {model.Email}.";
                    return View("Error", new ErrorViewModel { Title = errorTitle, Error = errorMessage, Link = confirmationLink });
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByEmailAsync(model.Email);

/*                if (
                    user != null
                    && !user.EmailConfirmed
                    && (await userManager.CheckPasswordAsync(user, model.Password))
                )
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View(model);
                }*/

                SignInResult signInResult = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (signInResult.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            User user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }

            return View("Error", new ErrorViewModel { Title = "Confirmation Error", Error = "Email cannot be confirmed" });
        }

        public async Task<IActionResult> IsEmailTaken(string email)
        {
            User foundUser = await userManager.FindByEmailAsync(email);
            if (foundUser == null)
            {
                return Json(true);
            } 
            else
            {
                return Json($"l'email ${email} est déjà pris.");
            }
        }
    }
}