﻿using AutoMapper;
using AyyBlog.ViewModel;
using Core.Entites;
using Core.Interfaces.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        public Authentication(UserManager<ApplicationUser> userManger, IUnitOfWork unitOfWork, IMapper mapper, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManger = userManger;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
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
            ViewData["ReturnUrl"] = returnUrl;
            return View("~/Views/Authentication/LoginRegister.cshtml");
        }

        [HttpPost("/Register")]
        public async Task<IActionResult> Register(LoginRegVM model)
        {
            if (model == null && model.email == null && model.password == null && model.userName==null) return View("~/Views/Authentication/LoginRegister.cshtml", model);


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
            //Create Default admin//

            if (model.email == null || model.password == null)
            {
                ModelState.AddModelError("name", "Fill the form");
                return View("~/Views/Authentication/LoginRegister.cshtml", model);
            }

            var user = await userManger.FindByEmailAsync(model.email);

            if (user != null&& await userManger.CheckPasswordAsync(user,model.password))
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("Email", model.email));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, model.email));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return RedirectToAction("aa", "Authentication");
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
            return Ok();
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
