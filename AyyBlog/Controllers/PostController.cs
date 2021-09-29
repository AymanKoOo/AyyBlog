using AutoMapper;
using AyyBlog.ViewModel;
using Core.Entites;
using Core.Interfaces.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AyyBlog.Controllers
{
    [Route("/[Controller]")]
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment environment;

        public PostController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("ShowPostBySlug")]
        public IActionResult ShowPostBySlug(string Slug)
        {
            var slug = _unitOfWork.Post.GetPostBySlug(Slug);
              
            return View(slug);
        }

        [Authorize]
        [HttpGet("CreatePost")]
        public IActionResult CreatePost()
        {

            return View();
        }

        [HttpPost("CreatePost")]
        public IActionResult CreatePost(PostDTO model)
        {
            if(!ModelState.IsValid) return View(model);


            string fileName = string.Empty;
            if (model.postImg != null)
            {
                string uploads = Path.Combine(environment.WebRootPath,"uploads");
                fileName = Guid.NewGuid() + Path.GetExtension(model.postImg.FileName).ToLower();
                string fullPath = Path.Combine(uploads, fileName);
                model.postImg.CopyToAsync(new FileStream(fullPath, FileMode.Create));
                model.picture = fileName;
                model.CategoryId = 1;
            }

            var post = _mapper.Map<Post>(model);
            _unitOfWork.Post.AddPost(post);
            _unitOfWork.save();
            return View();
        }


        [HttpPost("GetPosts")]
        public IActionResult GetPosts(int pageSize, int pageNumber)
        {
            if (pageSize < 1 || pageNumber < 1)
            {
                return BadRequest();
            }
            var posts = _unitOfWork.Post.GetPosts(pageSize, pageNumber);
            var postsValues = _mapper.Map<List<PostDTO>>(posts.postsData);

            var metadata = new
            {
                postsValues,
                posts.TotalCount,
                posts.PageSize,
                posts.CurrentPage,
                posts.TotalPages,
                posts.HasNext,
                posts.HasPrevious
            };
            return Ok(metadata);
        }
    }

    //    public class uploadsuccess
    //    { if (upload.Length <= 0) return null;

    //            //your custom code logic here

    //            //1)check if the file is image

    //            //2)check if the file is too large

    //            //etc

    //            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();

    //        //save file under wwwroot/CKEditorImages folder

    //        var filePath = Path.Combine(
    //            Directory.GetCurrentDirectory(), "wwwroot/images",
    //            fileName);

    //            using (var stream = System.IO.File.Create(filePath))
    //            {
    //                await upload.CopyToAsync(stream);
    //}

    //var url = $"{"/images/"}{fileName}";

    //var success = new uploadsuccess
    //{
    //    Uploaded = 1,
    //    FileName = fileName,
    //    Url = url
    //};

    //return new JsonResult(success);
    //public int Uploaded { get; set; }
    //        public string FileName { get; set; }
    //        public string Url { get; set; }
    //    }

}