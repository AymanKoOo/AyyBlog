using Core.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyyBlog
{
    public class MyIdentityDataInitializer
    {
        private readonly UserManager<ApplicationUser> _manager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public MyIdentityDataInitializer(UserManager<ApplicationUser> user,RoleManager<ApplicationRole> role)
        {

            _manager = user;
            _roleManager = role;
        }

        public void SeedData()
        {
            SeedRoles();
            SeedUsers();
        }

        public  void SeedUsers()
        {

            if (_manager.FindByNameAsync("Admin").Result==null)
            {
                var user = new ApplicationUser(){
                    
                    UserName = "Admin",
                    Email = "admin123@gmail.com"
                };

                var result = _manager.CreateAsync(user, "Mhnsaa123@").Result ;

                if (result.Succeeded)
                {
                    _manager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (_manager.FindByNameAsync("User").Result == null)
            {
                var user = new ApplicationUser()
                {
                    UserName = "User",
                    Email = "User123@gmail.com"
                };

                var result =  _manager.CreateAsync(user, "Mhnsaa123@").Result;

                if (result.Succeeded)
                {
                   _manager.AddToRoleAsync(user, "User").Wait();
                }
            }
        }


        public  void SeedRoles()
        {

            if ( !_roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new ApplicationRole();
                role.Name = "Admin";

                _roleManager.CreateAsync(role).Wait();
            }

            if (!_roleManager.RoleExistsAsync("User").Result)
            {
                var role = new ApplicationRole();
                role.Name = "User";
                _roleManager.CreateAsync(role).Wait();
            }
        }


        public void SeedCategory()
        {

        }

        public void SeedTags()
        {

        }
    }
}
