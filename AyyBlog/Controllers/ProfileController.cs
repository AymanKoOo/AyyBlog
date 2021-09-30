using AutoMapper;
using AyyBlog.ViewModel;
using Core.Interfaces.Base;
using Infrastructure.Repoo.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyyBlog.Controllers
{
    [Route("/[Controller]")]

    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProfileController(IUnitOfWork unitOfWork,IMapper mapper)
        {
         this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost("GetUserPosts")]
        public IActionResult GetPosts(int pageSize, int pageNumber)
        {
            string Useremn = User.Identity.Name;
            string Useremail = User.FindFirst("Email").Value;

            if (pageSize < 1 || pageNumber < 1)
            {
                return BadRequest();
            }
            var posts = unitOfWork.Post.GetUserPosts(pageSize, pageNumber, Useremail);

            //  var x = posts.postsData.FirstOrDefault(x => x.applicationUser.Email=="aa");

            var postsValues = mapper.Map<List<PostHomeDTO>>(posts.postsData);

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


        [HttpPost("HidePost")]
        public IActionResult HidePost(string Postslug)
        {
            var post = unitOfWork.Post.GetPostBySlug(Postslug);
            
            if (post != null)
            {
                post.visible = false;
                unitOfWork.save();
           
            }
            return Ok();
        }

        [HttpPost("ShowPost")]
        public IActionResult ShowPost(string Postslug)
        {
            var post = unitOfWork.Post.GetPostBySlug(Postslug);

            if (post != null)
            {
                post.visible = true;
                unitOfWork.save();
            }
            return Ok();
        }


    }
}
