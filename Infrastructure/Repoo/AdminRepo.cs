using Core.Entites;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repoo.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repoo
{
    public class AdminRepo : GenericRepo<ApplicationUser>, IAdminRepo
    {
        private readonly DataContext _dbcontext;
        private readonly UserManager<ApplicationUser> _user;

        public AdminRepo(DataContext dbcontext,UserManager<ApplicationUser> user) : base(dbcontext)
        {
            this._dbcontext = dbcontext;
            this._user = user;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _user.Users;
        }

        public void login()
        {
            throw new NotImplementedException();
        }
       
        
        public async Task<bool> IsEmailExist(string email)
        {

            var x = await _dbcontext.Users.AnyAsync(x => x.Email == email);

            if (x)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsUserNameExist(string userName)
        {
            var x = await _dbcontext.Users.AnyAsync(x => x.UserName == userName);

            if (x)
            {
                return true;
            }
            return false;
        }
    }
}
