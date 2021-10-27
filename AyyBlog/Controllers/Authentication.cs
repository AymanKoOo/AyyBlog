using AutoMapper;
using AyyBlog.ViewModel;
using Core.Entites;
using Core.Interfaces.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AyyBlog.Controllers
{
    [Route("/[Controller]")]

    public class Authentication : Controller
    {
        private readonly UserManager<ApplicationUser> userManger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment environment;

        public Authentication(UserManager<ApplicationUser> userManger, IWebHostEnvironment environment, IUnitOfWork unitOfWork, IMapper mapper, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManger = userManger;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.environment = environment;

        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet("/aa")]
        public IActionResult aa()
        {
            return View();
        }

        [HttpGet("/Auth")]
        public IActionResult Auth(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (Url.IsLocalUrl(ViewBag.ReturnUrl))
                    return Redirect(ViewBag.ReturnUrl);

                return RedirectToAction("Index", "Home");
            }
            return View("~/Views/Authentication/LoginRegister.cshtml");
        }

        [HttpPost("/Register")]
        public async Task<IActionResult> Register(LoginRegVM model)
        {
            if (model == null && model.email == null && model.password == null && model.userName==null) return View("~/Views/Authentication/LoginRegister.cshtml", model);

            string fileName = string.Empty;
            if (model.UserImgF != null)
            {
                string uploads = Path.Combine(environment.WebRootPath, "images\\UserProfileImage");
                fileName = Guid.NewGuid() + Path.GetExtension(model.UserImgF.FileName).ToLower();
                string fullPath = Path.Combine(uploads, fileName);
                await model.UserImgF.CopyToAsync(new FileStream(fullPath, FileMode.Create));
                model.ProfilePic = fileName;
            }
             
            var user = mapper.Map<ApplicationUser>(model);

            var result = await userManger.CreateAsync(user, model.password);

            if (!result.Succeeded)
            {
                foreach (var x in result.Errors)
                {
                    ModelState.TryAddModelError(x.Description, x.Code);
                }
                return View("~/Views/Authentication/LoginRegister.cshtml", model); ;
            }
            return Ok();
        }


        [HttpPost("/Login")]
        public async Task<IActionResult> Login(LoginRegVM model,string returnUrl)
        {
            if (returnUrl == null)
            {
                returnUrl = "~/Views/Authentication/LoginRegister.cshtml";
            }
            //Create Default admin//

            if (model.email == null || model.password == null)
            {
                ModelState.AddModelError("name", "Fill the form");
                return View("home", model);
            }

            var user = await userManger.FindByEmailAsync(model.email);

            if (user != null&& await userManger.CheckPasswordAsync(user,model.password))
            {

                var userObj = unitOfWork.Admin.GetUserObj(model.email);

                var claims = new List<Claim>();
                claims.Add(new Claim("Email", userObj.Email));
                claims.Add(new Claim("UserName", userObj.UserName));
                claims.Add(new Claim("About", userObj.About));

                claims.Add(new Claim(ClaimTypes.NameIdentifier, model.email));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError("name", "Email or Password is incorrect.");
                return View("~/Views/Authentication/LoginRegister.cshtml",model);
            }
        }
            
        [HttpGet("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [Route("IsUserNameExist/{userName}")]
        public async Task<bool> IsUserNameExist(string userName)
        {
            var exits =  unitOfWork.Admin.IsUserNameExist(userName);

            if (await exits)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        [Route("IsEmailExist/{email}")]
        public async Task<bool> IsEmailExist(string email)
        {
            var exits = await unitOfWork.Admin.IsEmailExist(email);

            if (exits)
            {
                return true;
            }
            return false;
        }

        //Forgot Password//
        //External Login Google//
        //Verfication//

    }

}
