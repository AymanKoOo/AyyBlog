using AutoMapper;
using AyyBlog.ViewModel;
using Core.Entites;
using Core.Interfaces.Base;
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
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> userManger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment environment;
    

        public PostController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManger, IMapper mapper, IWebHostEnvironment environment)
        {
            this._unitOfWork = unitOfWork;
            this.userManger = userManger;
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            DateTime todaysDate = DateTime.Today; // returns today's date
            string Useremail = User.FindFirst("Email").Value;
            // var user = _userManager.GetUserAsync(HttpContext.User).Result;
            var userobj = _unitOfWork.Admin.GetUserObj(Useremail);

            if (_unitOfWork.Post.TitleExists(model.title))
            {
                model.title += model.title + "1";
            };

            string fileName = string.Empty;
            if (model.postImg != null)
            {
                string uploads = Path.Combine(environment.WebRootPath, "uploads");
                fileName = Guid.NewGuid() + Path.GetExtension(model.postImg.FileName).ToLower();
                string fullPath = Path.Combine(uploads, fileName);
                model.postImg.CopyToAsync(new FileStream(fullPath, FileMode.Create));
                model.picture = fileName;
                model.CategoryId = 1;
                model.applicationUser = userobj;
                model.createdAt = todaysDate;
            }
            var post = _mapper.Map<Post>(model);
            _unitOfWork.Post.AddPost(post);
            _unitOfWork.save();
            return View();
        }


        [Authorize]
        [HttpGet("EditPost")]
        public IActionResult EditPost(string slug)
        {
            if (slug == null)
            {
                return BadRequest();
            }
            var post = _unitOfWork.Post.GetPostBySlug(slug);
            var postDto = _mapper.Map<PostDTO>(post);

            if (post == null)
            {
                return BadRequest();
            }
            return View(postDto);
        }

        [Authorize]
        [HttpPost("EditPost")]
        public IActionResult EditPost(PostDTO model)
        {
            if (!ModelState.IsValid) return View(model);
            var post = _unitOfWork.Post.GetPostBySlug(model.Slug);
            DateTime todaysDate = DateTime.Today; // returns today's date

            string fileName = string.Empty;
            if (model.postImg != null)
            {
                System.IO.File.Delete(environment.WebRootPath + "/uploads/" + post.picture);

                string uploads = Path.Combine(environment.WebRootPath, "uploads");
                fileName = Guid.NewGuid() + Path.GetExtension(model.postImg.FileName).ToLower();
                string fullPath = Path.Combine(uploads, fileName);
                model.postImg.CopyToAsync(new FileStream(fullPath, FileMode.Create));
                model.picture = fileName;
            }

            post.updatedAt = todaysDate;
            post.picture = fileName;

            post.title = model.title;
            post.summary = model.summary;
            post.Slug = model.Slug;
            post.content = model.content;

            _unitOfWork.Post.EditPost(post);

            _unitOfWork.save();

            return Ok();
        }

        [HttpPost("GetPosts")]
        public IActionResult GetPosts(int pageSize, int pageNumber)
        {
            if (pageSize < 1 || pageNumber < 1)
            {
                return BadRequest();
            }
            var posts = _unitOfWork.Post.GetPosts(pageSize, pageNumber);

            //  var x = posts.postsData.FirstOrDefault(x => x.applicationUser.Email=="aa");

            var postsValues = _mapper.Map<List<PostHomeDTO>>(posts.postsData);

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


        [Authorize]
        [HttpPost("PostComment")]
        public IActionResult PostComment(string comment, string postSlug)
        {
            if (comment == null && postSlug == null)
            {
                return BadRequest();
            }

            var user = userManger.GetUserId(User);
            var userObj = _unitOfWork.Admin.GetUserObj(user);
            var postObj = _unitOfWork.Post.GetPostBySlug(postSlug);

            Comment commentDto = new Comment
            {
                userId= userObj.Id,
                postId = postObj.id,
                post = postObj,
                user = userObj,
                content = comment,
                CreatedTime = DateTime.Now,
                replies = null
            };
            _unitOfWork.comment.AddComment(commentDto);
            _unitOfWork.save();
            
            return Ok();
        }

        [HttpPost("GetComments")]
        public IActionResult GetCommentsForPost(string postSlug)
        {
            if (postSlug == null)
            {
                return BadRequest();
            }

            var comments = _unitOfWork.comment.GetCommentsByPost(postSlug);

            var CommentDTO = _mapper.Map<List<CommentVM>>(comments);

            if (CommentDTO != null)
            {
                return Ok(CommentDTO);
            }
            else
            {
                return Ok("No Comments");
            }
        }

        [Authorize]
        [HttpPost("PostReply")]
        public IActionResult PostReply(string reply,int commentId)
        {
            if (reply == null)
            {
                return BadRequest();
            }
            //get comment
            //content 
            //date
            var user = userManger.GetUserId(User);
            var userObj = _unitOfWork.Admin.GetUserObj(user);

            var Rcomment = _unitOfWork.comment.GetCommentsByID(commentId);

            var model = new Reply
            {
                user = userObj,
                userId = userObj.Id,
                comment = Rcomment,
                commentID = Rcomment.Id,
                content = reply,
                CreatedTime = DateTime.Now
            };

            _unitOfWork.comment.AddReply(model);
            _unitOfWork.save();
            return Ok();
        }
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

