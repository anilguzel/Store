using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Rest;

namespace Store.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ActionResult Login()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginAndRegisterViewModel model)
        {
            using (HttpClient client = new HttpClient())
            {

                var uri = new Uri("https://localhost:44310/api/auth/login");

                var login = new LoginViewModel()
                {
                    Username = model.Login.Username,
                    Password = model.Login.Password
                };

                var jsonLogin = JsonConvert.SerializeObject(login);

                var result =
                    await client.PostAsync(uri, new StringContent(jsonLogin, Encoding.UTF8, "application/json"));
                var res = JsonConvert.SerializeObject(result.Content.ReadAsStringAsync().Result);
                res = res.Replace("\\\"\"", "");
                res = res.Replace("\"\\\"", "");

                var cookieOptions = new Microsoft.AspNetCore.Http.CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    IsEssential = true,
                    Expires = DateTime.UtcNow.AddDays(10)
                };

                var jwtToken = new JwtSecurityToken(res);
                List<Claim> claims = jwtToken.Payload.Claims.ToList();
                string claimType = claims[2].Type.ToString();
                //kullanici claim`i cookie`e eklenir.
                Response.Cookies.Append("claimType", claimType, cookieOptions);
                //token cookie`e eklenir.
                Response.Cookies.Append("authenticateUser", res, cookieOptions);
                //eger rememberMe secmis ise, userName cookie`e eklenir. her giris yapmak istediginde cookie`de kayitli userName var ise input`a yazar.
                if (model.Login.RememberMe)
                {
                    Response.Cookies.Append("userName", model.Login.Username, cookieOptions);
                }
            }
            return RedirectToAction("Index", "Product");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(LoginAndRegisterViewModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = new Uri("https://localhost:44310/api/auth/register");

                var jsonRegister = JsonConvert.SerializeObject(model.Register);

                var result = await client.PostAsync(uri, new StringContent(jsonRegister, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    if (result.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var message = await result.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, message);
                        return View();
                    }
                }
            }

            return View();
        }


        public async Task<ActionResult> Logout()
        {
            Response.Cookies.Delete("claimType");
            Response.Cookies.Delete("authenticateUser");


            using (HttpClient client = new HttpClient())
            {
                var uri = new Uri("https://localhost:44310/api/auth/logout");
                var result = await client.GetAsync(uri);
            }

            return RedirectToAction("Login");

        }
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            //email e sifre sifirlama linki
            return View();
        }
    }
}