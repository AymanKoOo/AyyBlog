using AyyBlog.ViewModel;
using Core.Entites;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repoo.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Infrastructure.Repoo
{
    public class PostRepo:GenericRepo<Post>,IPostRepo
    {
        private readonly DataContext dataContext;

        public PostRepo(DataContext dataContext):base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public void AddPost(Post model)
        {
            //Create Slug//
            string slugUrl = Core.Entites.Post.CreateSlug(model.title);
            model.Slug= slugUrl;
            dataContext.post.Add(model);
        }
        public void EditPost(Post model)
        {
            dataContext.Update(model);
          
        }
        public Post GetPostBySlug(string slug)
        {
             //dataContext.post.FirstOrDefault(m => m.Slug == slug).Include(s => s.applicationUser)
             //          .Include(e => e.Category); ;

            return dataContext.post
                       .Where(x => x.Slug == slug)
                       .Include(s => s.applicationUser)
                       .Include(e => e.Category).FirstOrDefault();
        }


        /// ///        /// ///        /// ///        /// ///        /// ///
        public IQueryable<Post> FindAll()
        {
            //  dataContext.post.Include(a => a.applicationUser);

            return dataContext.post
                       .Where(x=>x.visible==true)
                       .Include(s => s.applicationUser)
                       .Include(e => e.Category);
        }

        public PagedList<Post> GetPosts(int pageSize, int pageNumber)
        {
            return PagedList<Post>.ToPagedList(FindAll().OrderBy(on => on.title),
            pageNumber,
            pageSize);
        }

        /// ///        /// ///        /// ///        /// ///        /// ///

        public IQueryable<Post> FindSome(string email)
        {
            return dataContext.post
                     .Where(a => a.applicationUser.Email == email)
                     .Include(s => s.applicationUser).Include(e => e.Category); 
        }

        public PagedList<Post> GetUserPosts(int pageSize, int pageNumber,string email)
        {
            return PagedList<Post>.ToPagedList(FindSome(email).OrderBy(on => on.title),
            pageNumber,
            pageSize);
        }

        public bool DeletePostBySlug(string slug)
        {
            var post = GetPostBySlug(slug);
            if (post != null)
            {

                var result = dataContext.post.Remove(post);
                if (result != null)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public bool TitleExists(string title)
        {
           var exists = dataContext.post.Any(m => m.title == title);
            if (exists) return true;
            else return false;
        }
    }
}
