using Core.Entites;
using Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAdminRepo: IGenericRepo<ApplicationUser>
    {
        //Login
        public void login();

        public IEnumerable<ApplicationUser> GetAllUsers();
        public  Task<bool> IsUserNameExist(string userName);
        public  Task<bool> IsEmailExist(string userName);
    }
}
