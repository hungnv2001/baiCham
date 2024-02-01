using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppData;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DongVatsController : ControllerBase
    {
        private readonly MyContext _context;

        public DongVatsController(MyContext context)
        {
            _context = context;
        }

        // GET: api/DongVats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DongVat>>> GetDongVats()
        {
          if (_context.DongVats == null)
          {
              return NotFound();
          }
            return await _context.DongVats.ToListAsync();
        }

        // GET: api/DongVats/5
        [HttpGet("getByID/{id}")]
        public async Task<ActionResult<DongVat>> GetDongVat(Guid id)
        {
          if (_context.DongVats == null)
          {
              return NotFound();
          }
            var dongVat = await _context.DongVats.FindAsync(id);

            if (dongVat == null)
            {
                return NotFound();
            }

            return dongVat;
        }

        // PUT: api/DongVats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDongVat(Guid id, DongVat dongVat)
        {
            if (id != dongVat.Id)
            {
                return BadRequest();
            }

            _context.Entry(dongVat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DongVatExists(id))
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

        // POST: api/DongVats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DongVat>> PostDongVat(DongVat dongVat)
        {
          if (_context.DongVats == null)
          {
              return Problem("Entity set 'MyContext.DongVats'  is null.");
          }
            _context.DongVats.Add(dongVat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDongVat", new { id = dongVat.Id }, dongVat);
        } 
        [HttpPost("tinh_bmi")]
        public async Task<ActionResult<DongVat>> TinhBMI([FromForm]double height, [FromForm] double weight)
        {

            var bmi = Math.Round( weight / (height * height),2);
            return Ok(bmi);
            
        }

        // DELETE: api/DongVats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDongVat(Guid id)
        {
            if (_context.DongVats == null)
            {
                return NotFound();
            }
            var dongVat = await _context.DongVats.FindAsync(id);
            if (dongVat == null)
            {
                return NotFound();
            }

            _context.DongVats.Remove(dongVat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DongVatExists(Guid id)
        {
            return (_context.DongVats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
