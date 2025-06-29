using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public void SeedRoles()
        {
            if (_userManager.FindByEmailAsync(_configuration["SeedUsers:User:Email"]?? "").Result == null)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = _configuration["SeedUsers:User:UserName"];
                applicationUser.Email = _configuration["SeedUsers:User:Email"];
                applicationUser.NormalizedUserName = _configuration["SeedUsers:User:NormalizedUserName"];
                applicationUser.NormalizedEmail = _configuration["SeedUsers:User:NormalizedEmail"];
                applicationUser.EmailConfirmed = true;
                applicationUser.LockoutEnabled = false;
                applicationUser.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(applicationUser, _configuration["SeedUsers:User:Password"] ?? "").Result;
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(applicationUser, "User").Wait();
                }


            }
            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = _configuration["SeedUsers:Admin:UserName"];
                applicationUser.Email = _configuration["SeedUsers:Admin:Email"];
                applicationUser.NormalizedUserName = _configuration["SeedUsers:Admin:NormalizedUserName"];
                applicationUser.NormalizedEmail = _configuration["SeedUsers:Admin:NormalizedEmail"];
                applicationUser.EmailConfirmed = true;
                applicationUser.LockoutEnabled = false;
                applicationUser.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(applicationUser, _configuration["SeedUsers:Admin:Password"] ?? "").Result;
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(applicationUser, "Admin").Wait();
                }


            }
        }

        public void SeedUsers()
        {
            if (!_roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                role.NormalizedName = "User";
                IdentityResult result = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "Admin";
                IdentityResult result = _roleManager.CreateAsync(role).Result;
            }
        }
    }
}
