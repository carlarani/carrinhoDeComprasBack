using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using carrinhoBack.Context;
using carrinhoBack.Models;

namespace carrinhoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoComprasController : ControllerBase
    {
        private readonly CarrinhoContext _context;

        public ProdutoComprasController(CarrinhoContext context)
        {
            _context = context;
        }

        // GET: api/ProdutoCompras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoCompra>>> GetProdutoCompra()
        {
            return await _context.ProdutoCompra.ToListAsync();
        }

        // GET: api/ProdutoCompras/Compras
        [HttpGet("Compra/{idCompra}")]
        public async Task<ActionResult<IEnumerable<ProdutoCompra>>> GetProdutoCompraPorIdCompra(Guid idCompra)
        {
            return await _context.ProdutoCompra.Where((x)=> x.IdCompra==idCompra).ToListAsync();
        }

        // GET: api/ProdutoCompras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoCompra>> GetProdutoCompra(Guid id)
        {
            var produtoCompra = await _context.ProdutoCompra.FindAsync(id);

            if (produtoCompra == null)
            {
                return NotFound();
            }

            return produtoCompra;
        }

        // PUT: api/ProdutoCompras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdutoCompra(Guid id, ProdutoCompra produtoCompra)
        {
            if (id != produtoCompra.Id)
            {
                return BadRequest();
            }

            _context.Entry(produtoCompra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoCompraExists(id))
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

        // POST: api/ProdutoCompras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProdutoCompra>> PostProdutoCompra(ProdutoCompra produtoCompra)
        {
            _context.ProdutoCompra.Add(produtoCompra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProdutoCompra", new { id = produtoCompra.Id }, produtoCompra);
        }

        // DELETE: api/ProdutoCompras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdutoCompra(Guid id)
        {
            var produtoCompra = await _context.ProdutoCompra.FindAsync(id);
            if (produtoCompra == null)
            {
                return NotFound();
            }

            _context.ProdutoCompra.Remove(produtoCompra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoCompraExists(Guid id)
        {
            return _context.ProdutoCompra.Any(e => e.Id == id);
        }
    }
}
