using carrinhoBack.Context;
using carrinhoBack.Models;
using Microsoft.EntityFrameworkCore;

namespace carrinhoBack.Repository
{
    public class ProdutoRepository
    {
        private readonly CarrinhoContext _context;

        public ProdutoRepository(CarrinhoContext context) {
            
            _context = context;

        }

        public Task<List<Produto>> GetProdutosComPag(int paginaSelecionda, int resultadosPorPagina)
        {
            return Task.Run(() =>
            {
                var produtos = _context.Produtos
                .OrderBy(x=> x.Nome)
                .Skip((paginaSelecionda - 1) * resultadosPorPagina)
                .Take(resultadosPorPagina).ToList();
                return produtos;
            });
        }

        public Task<List<Produto>> GetProdutosComPagEComFiltro(int paginaSelecionda, int resultadosPorPagina, string produtoFiltro)
        {
            return Task.Run(() =>
            {
                var produtos = _context.Produtos
                .Where(x=> x.Nome.Contains(produtoFiltro))
                .OrderBy(x => x.Nome)
                .Skip((paginaSelecionda - 1) * resultadosPorPagina)
                .Take(resultadosPorPagina).ToList();
                return produtos;
            });
        }
    }
}
