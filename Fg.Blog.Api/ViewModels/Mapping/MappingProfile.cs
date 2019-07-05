using AutoMapper; 

namespace Fg.Blog.Api.ViewModels.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Model.Blog, BlogDetailViewModel>()
                .ForMember(s => s.OwnerUsername, map => map.MapFrom(s => s.Owner.Username));
                 
            CreateMap<Model.Blog, OwnerBlogViewModel>();
            CreateMap<Model.Blog, BlogViewModel>()
                .ForMember(s => s.OwnerUsername, map => map.MapFrom(s => s.Owner.Username));
        }
    }
}