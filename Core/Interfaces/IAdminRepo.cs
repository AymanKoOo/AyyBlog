using Core.Entites;
using Core.Interfaces.Base;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public string GetUserId(string email);
        public ApplicationUser GetUserObj(string email);
        public ApplicationUser GetUserObjBySlug(string slug);
    }
}
