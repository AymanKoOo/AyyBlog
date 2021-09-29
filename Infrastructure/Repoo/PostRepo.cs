using AyyBlog.ViewModel;
using Core.Entites;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repoo.Base;
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
        public IQueryable<Post> FindAll()
        {
            return dataContext.Set<Post>();
        }

        public Post GetPostBySlug(string slug)
        {
            return dataContext.post.FirstOrDefault(m => m.Slug == slug);
        }

        public PagedList<Post> GetPosts(int pageSize, int pageNumber)
        {
            return PagedList<Post>.ToPagedList(FindAll().OrderBy(on => on.title),
            pageNumber,
            pageSize);
        }

        
    }
}
