using APIProject.Core.Entities;
using APIProject.Service.Dtos.Books;
using APIProject.Service.Dtos.Categories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIProject.Service.Profiles.Books
{
    public class BookProfile:Profile
    {
        public BookProfile()
        {
            CreateMap<BookPostDto, Book>();
            CreateMap<BookUpdateDto, Book>();
        }
    }
}
