using DataAccesLayer.Concrete;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreeMenuService.Controllers
{
    [Route("api/categories")] 
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly Context _context;

        public CategoryController(Context context)
        {
            _context = context;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.Categories
                .Include(c => c.Children) 
                .ToListAsync();

            return Ok(categories);
        }


        [HttpGet("main")] 
        public async Task<IActionResult> GetMainCategories()
                                                    {    
            var categories = await _context.Categories
                .Where(c => c.CategoryId == null) 
                .Include(c => c.Children) 
                .ToListAsync();
            return Ok(categories);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetCategoriesByCategoryId(int categoryId)
        {
            var categories = await _context.Categories
                .Where(c => c.CategoryId == categoryId) 
                .Include(c => c.Children) 
                .ToListAsync();

            return Ok(categories);
        }



        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMainCategories), new { id = category.Id }, category);
        }

     
        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateCategory(int id, Category updatedCategory)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            category.Label = updatedCategory.Label;
            category.Icon = updatedCategory.Icon;
            category.Url=updatedCategory.Url;
            await _context.SaveChangesAsync();
            return Ok(category);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Children) 
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound();


            if (category.Children != null && category.Children.Any())
            {
                _context.Categories.RemoveRange(category.Children);
            }

        
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok();
        }



        [HttpPost("{categoryId}/subcategories")]
        public async Task<IActionResult> AddSubCategory(int categoryId, Category subCategory)
        {
            var categoryCategory = await _context.Categories.FindAsync(categoryId);
            if (categoryCategory == null) return NotFound();

            subCategory.CategoryId = categoryId;
            _context.Categories.Add(subCategory);
            await _context.SaveChangesAsync();
            return Ok(subCategory);
        }
    }
}
