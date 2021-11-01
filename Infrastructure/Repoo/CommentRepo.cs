using Core.Entites;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repoo.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repoo
{
    public class CommentRepo : GenericRepo<Comment>, ICommentRepo
    {
        private readonly DataContext dataContext;

        public CommentRepo(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public void AddComment(Comment model)
        {
            if (model != null)
            {
                dataContext.Comment.Add(model);
            }
        }

        public void DeleteComment(Comment comment)
        {
            if (comment != null)
            {
                dataContext.Comment.Remove(comment);
            }
        }

        public List<Comment> GetCommentsByPost(string postSlug)
        {
            return dataContext.Comment.Where(x => x.post.Slug == postSlug).Include(s => s.user).Include(e => e.post)
                .Include(b=>b.replies)
         
                .ToList();
        }

        //////////////////////////////////////////////////
        //////////////////////////////////////////////////

        public void AddReply(Reply model)
        {
            if (model != null)
            {
                dataContext.Reply.Add(model);
            }
        }

        public Comment GetCommentsByID(int commentID)
        {
            return dataContext.Comment.Where(x => x.Id == commentID).FirstOrDefault();
        }
    }
}
