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

            CreateMap<Post, PostHomeDTO>()
             .ForMember
              (x => x.UserName,
              map => map.MapFrom(source => source.applicationUser.UserName))

            .ForMember
              (e => e.email,
              map => map.MapFrom(source => source.applicationUser.Email))

             .ForMember
              (e => e.About,
              map => map.MapFrom(source => source.applicationUser.About));
            
            CreateMap<PostHomeDTO, Post>();
        }
    }
}
