using AutoMapper;
using AyyBlog.ViewModel;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();

            CreateMap<ApplicationUser, LoginRegVM>();
            CreateMap<LoginRegVM, ApplicationUser>();

            CreateMap<IEnumerable<LoginRegVM>,IEnumerable<ApplicationUser>>();
            CreateMap<IEnumerable<ApplicationUser>, IEnumerable<LoginRegVM>>();

            CreateMap<List<PostDTO>, List<Post>>();
        }
    }
}
