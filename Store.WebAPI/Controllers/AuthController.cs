using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Store.DataAccess.Models.IdentityModels;
using Store.WebAPI.Models;

namespace Store.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IPasswordHasher<ApplicationUser> _hasher;
        private IConfiguration _config;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;


        public AuthController(IConfiguration config, UserManager<ApplicationUser> userManager,
            IPasswordHasher<ApplicationUser> hasher, SignInManager<ApplicationUser> signInManager)
        {
            _config = config;
            _userManager = userManager;
            _hasher = hasher;
            _signInManager = signInManager;
        }

        #region login

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredentialModel model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Succeeded) //succeeded olmadi ise, username password hatali.
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    if (user != null)
                    {
                        var tokenPacket = CreateToken(user);
                        if (tokenPacket != null && tokenPacket.Result != null)
                        {
                            return Ok(tokenPacket.Result);
                        }
                    }
                }
            }
            catch (Exception)
            {
                //log
            }

            return BadRequest("login basarili olamadi!");
        }

        #endregion

        #region register

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("girdi hatasi");

            }

            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    user = new ApplicationUser()
                    {
                        Name = model.FirstName,
                        Surname = model.LastName,
                        UserName = model.UserName,
                        Email = model.Email
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return Ok(CreateToken(user));
                    }
                    else
                    {
                        return BadRequest(result.Errors);
                    }
                }
                else
                {
                    return BadRequest("bu kullanici adi kullanimda");
                }
            }
            catch (Exception ex)
            {

                //log
                return BadRequest($"beklenmeyen hata!:{ex}");
            }
        }

        #endregion

        #region createToken

        [HttpPost("token")]
        public async Task<object>
            CreateToken([FromBody] CredentialModel model) //kimlik = username-password bilgilerini alir.
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model
                    .UserName); // username ile veritabaninda kullanici aranir.
                if (user != null) // eger var ise
                {
                    if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) ==
                        PasswordVerificationResult.Success) // password`lar uyusuyor ise
                    {
                        return await CreateToken(user); // token`i gelen kullanici bilgileri icin yarat!
                    }
                }
            }
            catch (Exception)
            {
                //log 
            }

            return null;
        }

        private async Task<object> CreateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user); // kullanicinin db`de tanimli claimleri cekilir. 

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName), //claim tanimlaniyor
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // id tanimlanir
            }.Union(userClaims); // db`den cekilen claimler ile yukarida tanimlanan claimleri birlestir

            //appsettings icindeki Tokens`e git, alttaki var degiskenler atanir.
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Tokens:JwtKey"])); //tokens key parametresi ile key olusturulur.
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //key imzalanir.
            var token = new JwtSecurityToken(
                issuer: _config["Tokens:JwtIssuer"], //token yaratilir.
                audience: _config["Tokens:JwtIssuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(10),
                signingCredentials: cred
            );
            return new JwtSecurityTokenHandler().WriteToken(token); //sadece token
            // alttaki yorum satirindaki kodlar, return new JwtSecurityTokenHandler().WriteToken(token) kodunun alternatifi olarak,
            // client`a jwt gonderilirken beraberinde "hangi bilgiler gonderilsin" ayarlaniyor. ancak zaten jwt token decode edilerek istenilen bilgiler alinabiliyor.
            //return new JwtPacket
            //{
            //    Token = new JwtSecurityTokenHandler().WriteToken(token),
            //    Expiration = token.ValidTo.ToString(),
            //    UserName = user.UserName
            //};

        }
    }

    public class JwtPacket
    {
        public string Token;
        public string UserName;
        public string Expiration;
    }
}

#endregion
