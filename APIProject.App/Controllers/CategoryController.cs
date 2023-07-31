using APIProject.Core.Entities;
using APIProject.Core.Repositories;
using APIProject.Data.Repositories;
using APIProject.Service.Dtos.Categories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProject.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Category> categories = await _repository.GetAllAsync(x => !x.IsDeleted);
            return Ok(categories);
            //var result = await _service.GetAllAsync();
            //return StatusCode(result.StatusCode, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Category category = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CategoryPostDto dto)
        {
            if (await _repository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return BadRequest();
            }
            Category category = _mapper.Map<Category>(dto);
            category.CreatedAt = DateTime.Now;
            await _repository.AddAsync(category);
            await _repository.SaveAsync();
            return Ok(category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody]CategoryUpdateDto dto)
        {
            if (await _repository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return BadRequest();
            }
            Category category = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (category is null)
            {
                return NotFound();
            }
            category.UpdatedAt = DateTime.UtcNow.AddHours(4);
            category.Name = dto.Name;
            await _repository.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Category category =await _repository.GetAsync(x => x.Id == id);
            if(category is null)
            {
                return NotFound();
            }
            category.IsDeleted = true;
            await _repository.SaveAsync();
            return Ok(category);
        }
    }
}
