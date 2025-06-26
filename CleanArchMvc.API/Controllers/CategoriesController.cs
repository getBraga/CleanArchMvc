using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
     
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
      
        }
        [HttpGet]   
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _categoryService.GetCategories();
            return Ok(categories ?? []);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null) return NotFound("Category not found!");
            return Ok(category);
        }
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post(CategoryDTO category) {

            if (category == null) return BadRequest("Invalid Data");
            var categoryResult = await _categoryService.Add(category);
        
            return Ok(categoryResult);
        
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, CategoryDTO category)
        {
            if (id != category.Id || category == null) return BadRequest();

            var categoryUpdate = await _categoryService.Update(category);

            if (categoryUpdate == null) return NotFound("Category not found");
            return Ok(categoryUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null) return NotFound("Category not found");
            await _categoryService.Remove(id);
            return Ok("Category was successfully removed.");
        }

 
       

    }
}
