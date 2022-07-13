using API.Models;
using Application.Common.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMongoDbContext _mongoDbContext;
        private readonly IMongoCollection<Category> _categoryCollection;
        public CategoryController(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _categoryCollection = _mongoDbContext.GetCollection<Category>(nameof(Category));
        }
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryDto categoryDto)
        {
            Category category = new Category
            {
                Name = categoryDto.Name,
                Limit = categoryDto.Limit,
                LimitPeriod = categoryDto.LimitPeriod,
                SubCategories = categoryDto.SubCategories,
                CategoryType = categoryDto.CategoryType,
                Color = categoryDto.Color
            };
            await _categoryCollection.InsertOneAsync(category);
            return Ok(categoryDto);
        }
        [HttpGet("ReadCategory")]
        public async Task<IActionResult> ReadAllCategories()
        {
            var filterBuilder = Builders<Category>.Filter;
            var filter = filterBuilder.Empty;
            return Ok((await _categoryCollection.FindAsync(filter)).ToList());
        }
    }
}
