using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection.Emit;
using carrinhoBack.Models;

namespace carrinhoBack.Context
{
    public class CarrinhoContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Compra> Compras { get; set; }

        public DbSet<ProdutoCompra> ProdutoCompras { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public CarrinhoContext(DbContextOptions<CarrinhoContext> options) : base(options)
        {

        }

        public DbSet<carrinhoBack.Models.ProdutoCompra> ProdutoCompra { get; set; }

    }
}
