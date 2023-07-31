using APIProject.Core.Entities;
using APIProject.Core.Repositories;
using APIProject.Service.Dtos.Books;
using APIProject.Service.Dtos.Categories;
using APIProject.Service.Extensions;
using APIProject.Service.Responses;
using APIProject.Service.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace APIProject.Service.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _evn;
        private readonly IHttpContextAccessor _http;

        public BookService(IBookRepository repository, IMapper mapper, ICategoryRepository categoryRepository, IWebHostEnvironment evn, IHttpContextAccessor http)
        {
            _repository = repository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _evn = evn;
            _http = http;
        }

        public async Task<ApiResponse> CreateAsync(BookPostDto dto)
        {
            if (!await _categoryRepository.isExsist(x => x.Id == dto.CategoryId))
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Category not Found"
                };
            }
            Book book = _mapper.Map<Book>(dto);
            book.Image = dto.file.CreateImage(_evn.WebRootPath, "assests/images/books");
            book.ImageUrl = _http.HttpContext.Request.Scheme + "://" + _http.HttpContext.Request.Host
                + $"assests/images/books/{book.Image}";
            await _repository.AddAsync(book);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = book
            };
        }
        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<Book> books = await _repository.GetAllAsync(x => !x.IsDeleted);
            return new ApiResponse
            {
                items = books,
                StatusCode = 200
            };
        }
        public async Task<ApiResponse> GetAsync(int id)
        {
            Book book = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (book is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            return new ApiResponse
            {
                StatusCode = 200,
                items = book
            };
        }
        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Book book = await _repository.GetAsync(x => x.Id == id);
            if (book is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            book.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = book
            };
        }
        public async Task<ApiResponse> UpdateAsync(int id, BookUpdateDto dto)
        {
            if (!await _categoryRepository.isExsist(x => x.Id == dto.CategoryId))
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Category not Found"
                };
            }
            Book book = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (book is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            book.UpdatedAt = DateTime.UtcNow.AddHours(4);
            book.Name = dto.Name;
            book.Author = dto.Author;
            book.Price = dto.Price;
            book.CategoryId = dto.CategoryId;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = book
            };
        }
    }
}
