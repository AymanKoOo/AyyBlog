using Core.Entites;
using Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface ICommentRepo:IGenericRepo<Comment>
    {
        public void AddComment(Comment comment);
        public void DeleteComment(Comment comment);
        public List<Comment> GetCommentsByPost(string postSlug);
        public Comment GetCommentsByID(int commentID);

        public void AddReply(Reply model);
    }
}
