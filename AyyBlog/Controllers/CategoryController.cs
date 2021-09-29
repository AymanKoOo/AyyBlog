using AutoMapper;
using Core.Entites.Base;
using Core.Interfaces.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyyBlog.Controllers
{
    [Route("/[Controller]")]

    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment environment;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("CreateCategory")]
        public IActionResult CreateCategory()
        {

            return View();
        }

        [HttpPost("CreateCategoryP")]
        public IActionResult CreateCategoryP()
        {
            //if (!ModelState.IsValid) return View(model);

            var categ = new category
            {
                title="SOON",
                content="SOON"
            };

            _unitOfWork.Category.AddCat(categ);
            _unitOfWork.save();
            return Ok();
        }
    }
}
