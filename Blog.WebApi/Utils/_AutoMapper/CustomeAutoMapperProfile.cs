using AutoMapper;
using Blog.Model;
using Blog.WebApi.Utils.Model;

namespace Blog.WebApi.Utils._AutoMapper
{
    public class CustomeAutoMapperProfile : Profile
    {
        public CustomeAutoMapperProfile()
        {
            base.CreateMap<Author, AuthorDto>();

            base.CreateMap<BlogNews, BlogNewsDto>()
                .ForMember(d => d.AuthorName, s => s.MapFrom(src => src.Author.Name))
                .ForMember(d => d.TypeName, s => s.MapFrom(src => src.BlogType.TypeName));
        }
    }
}
