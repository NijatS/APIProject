using APIProject.Service.Dtos.Books;
using APIProject.Service.Dtos.Categories;
using APIProject.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIProject.Service.Services.Interfaces
{
    public interface IBookService
    {
        public Task<ApiResponse> CreateAsync(BookPostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> UpdateAsync(int id, BookUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
