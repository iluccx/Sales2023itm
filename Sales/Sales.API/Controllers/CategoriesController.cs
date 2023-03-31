using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.API.Helperds;
using Sales.Shared.DTO;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("/api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        } 


        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Categories
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        //Agregar registros
        [HttpPost]
        public async Task<ActionResult> PostAsync(Category categories)
        {
            try
            {
                _context.Categories.Add(categories);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un registro con el mismo nombre.");
                }
                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(Category category)
        {
            try
            {
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un registro con el mismo nombre.");
                }
                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
