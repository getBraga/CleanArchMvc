using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;
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

        public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void SeedRoles()
        {
            if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = "usuario@localhost";
                applicationUser.Email = "usuario@localhost";
                applicationUser.NormalizedUserName = "USUARIO@LOCALHOST";
                applicationUser.NormalizedEmail = "USUARIO@LOCALHOST";
                applicationUser.EmailConfirmed = true;
                applicationUser.LockoutEnabled = false;
                applicationUser.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(applicationUser, "Numsey#2021").Result;
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(applicationUser, "User").Wait();
                }


            }
            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = "admin@localhost";
                applicationUser.Email = "admin@localhost";
                applicationUser.NormalizedUserName = "ADMIN@LOCALHOST";
                applicationUser.NormalizedEmail = "ADMIN@LOCALHOST";
                applicationUser.EmailConfirmed = true;
                applicationUser.LockoutEnabled = false;
                applicationUser.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(applicationUser, "Numsey#2021").Result;
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
