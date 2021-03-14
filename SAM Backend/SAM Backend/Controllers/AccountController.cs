﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SAM_Backend.Models;
using SAM_Backend.Services;
using SAM_Backend.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAM_Backend.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ILogger<AccountController> logger;
        private readonly IJWTService jWTService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger<AccountController> logger, IJWTService jWTService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.jWTService = jWTService;
        }

        [HttpPost]
        public async Task<ActionResult> Signup(SignupViewModel model)
        {
            #region check email/username
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null) return BadRequest("There is already an account with this email address");
            user = await userManager.FindByNameAsync(model.Username);
            if (user != null) return BadRequest("Username is not available");
            #endregion

            #region Signup attempt
            var newUser = new AppUser() { Email = model.Email, UserName = model.Username };
            var result = await userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
                return BadRequest(ModelState);
            }
            #endregion

            #region EmailConfirmation Link
            var EmailConfirmationTokoen = await userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var ConfirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { EmailConfirmationTokoen }, Request.Scheme);
            // TODO: Send email
            logger.LogInformation("EmailConfirmation Link: ${}", ConfirmationLink);
            return Ok(new AppUserViewModel(newUser));
            #endregion
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmEmail()
        {
            return null;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            #region Find user
            AppUser user = model.IsEmail ? await userManager.FindByEmailAsync(model.Identifier) : await userManager.FindByNameAsync(model.Identifier);
            if (user == null) return NotFound("User not found!");
            #endregion

            #region Attempt Signin
            var result = await signInManager.PasswordSignInAsync(user, model.Password, true, true);
            if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "Email is not confirmed");
                return Unauthorized(ModelState);
            }
            if (result.IsLockedOut)
            {
                return StatusCode(423, "Too many Failed attempts! please try later.");
            }

            if (!result.Succeeded)
            {
                return BadRequest("Inavlid login attempt");
            }
            #endregion

            #region Send JWT
            // TODO: remove expired tokens
            var token = jWTService.GenerateToken(user);
            return Ok(token);
            #endregion
        }


        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            // TODO: insert token to black list
            return Ok();
        }
    }
}
