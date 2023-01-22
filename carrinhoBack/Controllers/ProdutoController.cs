using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using carrinhoBack.Models;
using carrinhoBack.Context;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using carrinhoBack.Repository;

namespace carrinhoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly CarrinhoContext _context;
        private readonly ProdutoRepository _produtoRepository;


        public ProdutoController(CarrinhoContext context, ProdutoRepository produtoRepository)
        {
            _context = context;
            _produtoRepository = produtoRepository;
        }

        // GET: api/Produto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _context.Produtos
                .OrderBy(x=> x.Nome)
                .ToListAsync();
        }

        //GET com paginação : api/Produto/pag/{paginaSelecionda}/res/{resultadoPorPagina}

        [HttpGet("pag/{paginaSelecionda}/res/{resultadosPorPagina}")]
        public async Task<ActionResult<IEnumerable<Produto>>> Get([FromRoute] int paginaSelecionda, [FromRoute] int resultadosPorPagina)
        {
            var produtos = await _produtoRepository.GetProdutosComPag(paginaSelecionda, resultadosPorPagina);
            return Ok(produtos);
        }

        //GET com paginação e filtro : api/Produto/pag/{paginaSelecionda}/res/{resultadoPorPagina}/filtro/{ProdutoFiltro}

        [HttpGet("pag/{paginaSelecionda}/res/{resultadosPorPagina}/filtro/{produtoFiltro}")]
        public async Task<ActionResult<IEnumerable<Produto>>> Get([FromRoute] int paginaSelecionda, [FromRoute] int resultadosPorPagina, [FromRoute] string produtoFiltro)
        {
            var produtos = await _produtoRepository.GetProdutosComPagEComFiltro(paginaSelecionda, resultadosPorPagina, produtoFiltro);
            return Ok(produtos);
        }

        // GET: api/Produto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        // PUT: api/Produto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(Guid id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
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

        // POST: api/Produto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            if (Guid.Equals(produto.Id, new Guid("e948e219-b42f-4829-8788-94723c335def")))
                produto.Id = new Guid();
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
        }

        // DELETE: api/Produto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoExists(Guid id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }

    }
}
