using MyBlog.Applications.Auth;
using MyBlog.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MyBlog.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthService _auth;
        public LoginController(IAuthService auth)
        {
            _auth = auth;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest model)
        {
            if (await _auth.LoginUserCheckPwd(model))
            {
                var claims = new List<Claim>
                {
                    new Claim("UserCode",model.Pwd)
                };
                
                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme,
                   new ClaimsPrincipal(claimsIdentity),
                   new AuthenticationProperties
                   {
                       ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1),
                       IsPersistent = true,
                   });

                return Redirect("/");
            }

            return View();
        }
    }
}
