using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MW.Application.Interfaces.Repositories;
using MW.Application.Models.Membership;
using MW.Application.Utilities.Helpers;
using MW.Domain.Entities.Membership;
using System.Security.Claims;

namespace MW.Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Login()
        {
            if(HttpContext.User == null)
                return RedirectToAction("Home","Index");
            else
                return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState.FirstOrDefault().Value?.Errors.FirstOrDefault()?.ErrorMessage);

            User user = await _unitOfWork.Users.GetByEmailAsync(loginModel.Email);

            if (user == null || user.Deleted)
                return NotFound("E-posta adresi veya şifre yanlış!");

            if (!PasswordHashHelper.VerifyPassword(loginModel.Password, user.PasswordHash))
                return Unauthorized("E-posta adresi veya şifre yanlış!");

            if (!user.Active)
                return Forbid("Kullanıcınız pasif durumdadır!");

            await SignInAsync(user);
            return Ok();
        }
        public IActionResult Logout()
        {
            if(HttpContext.User != null)
                HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }
        private async Task SignInAsync(User user)
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim("FullName", user.FirstName + " " + user.LastName),
            new Claim("Avatar", user.AvatarImageName),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

           await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}
