﻿using AyyBlog.ViewModel;
using Core.Entites;
using Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Interfaces
{
    public interface IPostRepo : IGenericRepo<Post>
    {
        public void AddPost(Post model);
        public void EditPost(Post model);

        public PagedList<Post> GetPosts(int pageSize, int pageNumber);
        public Post GetPostBySlug(string slug);
        public bool DeletePostBySlug(string slug);
        public bool TitleExists(string title);

        public PagedList<Post> GetUserPosts(int pageSize,int pageNumber, string email);

    }
}
