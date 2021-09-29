using AutoMapper;
using AyyBlog.ViewModel;
using Core.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyyBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]")]

    public class Users : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Users(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("ShowUsers")]
        public IActionResult ShowUsers()
        {
            var users = _unitOfWork.Admin.GetAllUsers();
            
           // var mappedUsers = _mapper.Map<IEnumerable<UserDTO>>(users); 

            return View(users);
        }


        [HttpGet("UserProfile")]
        public IActionResult UserProfile()
        {
            var users = _unitOfWork.Admin.GetAllUsers();

            // var mappedUsers = _mapper.Map<IEnumerable<UserDTO>>(users); 

            return View(users);
        }


    }
}
