using AutoMapper;
using BookstoreSystem.DTOs;
using BookstoreSystem.Models;

namespace BookstoreSystem.Mappings
{
    public class EntitiesToDTOMAppingProfile : Profile
    {
        public EntitiesToDTOMAppingProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<CreateBookDTO, Book>();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Publisher, PublisherDTO>().ReverseMap();
            CreateMap<Writer, WriterDTO>().ReverseMap();
        }
    }
}
