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
        }
    }
}
