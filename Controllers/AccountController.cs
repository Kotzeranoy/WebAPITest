using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Models;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TestDBContext _context;
        public AccountController(TestDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetMyEntities()
        {
            return await _context.Employees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetMyEntity(int id)
        {
            var myEntity = await _context.Employees.FindAsync(id);

            if (myEntity == null)
            {
                return NotFound();
            }

            return myEntity;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostMyEntity(Employee emp)
        {
            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMyEntity", new { id = emp.Id }, emp);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMyEntity(int id, Employee emp)
        {
            if (id != emp.Id)
            {
                return BadRequest();
            }

            _context.Entry(emp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyEntity(int id)
        {
            var myEntity = await _context.Employees.FindAsync(id);
            if (myEntity == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(myEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MyEntityExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}