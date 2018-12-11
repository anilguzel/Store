using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Store.DataAccess.Models.IdentityModels
{
    public class ApplicationUserDbSeeder
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public ApplicationUserDbSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedUserAndRolesClaims()
        {
            var user = await _userManager.FindByNameAsync("jouverc2");
            if (user == null)
            {
                if (!(await _roleManager.RoleExistsAsync("product_view")))
                {
                    //var role = new IdentityRole("Admin");
                    //role.Claims.Add(new IdentityRoleClaim<string>
                    //{
                    //    ClaimType = "IsAdmin",
                    //    ClaimValue = "True"
                    //});
                    var role = new IdentityRole("product_view");
                    await _roleManager.CreateAsync(role);
                    //await _roleManager.AddClaimAsync(role, new Claim("IsAdmin", "True"));
                }
                user = new ApplicationUser()
                {
                    UserName = "jouverc2",
                    Name = "anil",
                    Surname = "guzel",
                    Email = "anilguzel95@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString()

                };
                var userResult = await _userManager.CreateAsync(user, "P@ssw0rd!");
                var roleResult = await _userManager.AddToRoleAsync(user, "product_view");
                var claimResult = await _userManager.AddClaimAsync(user, new Claim("product_view", "True"));
                if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
                {
                    throw new InvalidOperationException("kullanici yaratmada sorun var!");
                }
            }
        }
    }
}