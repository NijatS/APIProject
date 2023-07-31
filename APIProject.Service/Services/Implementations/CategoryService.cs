using APIProject.Service.Dtos.Categories;
using APIProject.Service.Responses;
using APIProject.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIProject.Service.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        public Task<ApiResponse> CreateAsync(CategoryPostDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UpdateAsync(int id, CategoryUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
