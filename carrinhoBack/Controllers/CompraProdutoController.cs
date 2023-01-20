using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using carrinhoBack.Models;
using carrinhoBack.Context;

namespace carrinhoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraProdutoController : ControllerBase
    {
        private readonly CarrinhoContext _context;

        public CompraProdutoController(CarrinhoContext context)
        {
            _context = context;
        }

        // GET: api/CompraProduto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompraProduto>>> GetCompraProduto()
        {
            return await _context.CompraProduto.ToListAsync();
        }

        // GET: api/CompraProduto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompraProduto>> GetCompraProduto(int id)
        {
            var compraProduto = await _context.CompraProduto.FindAsync(id);

            if (compraProduto == null)
            {
                return NotFound();
            }

            return compraProduto;
        }

        // PUT: api/CompraProduto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompraProdutol(int id, CompraProduto compraProduto)
        {
            if (id != compraProduto.Id)
            {
                return BadRequest();
            }

            _context.Entry(compraProduto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompraProdutoExists(id))
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

        // POST: api/CompraProduto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompraProduto>> PostCompraProduto(CompraProduto compraProduto)
        {
            _context.CompraProduto.Add(compraProduto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompraProduto", new { id = compraProduto.Id }, compraProduto);
        }

        // DELETE: api/CompraProduto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompraProduto(int id)
        {
            var compraProduto = await _context.CompraProduto.FindAsync(id);
            if (compraProduto == null)
            {
                return NotFound();
            }

            _context.CompraProduto.Remove(compraProduto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompraProdutoExists(int id)
        {
            return _context.CompraProduto.Any(e => e.Id == id);
        }
    }
}
