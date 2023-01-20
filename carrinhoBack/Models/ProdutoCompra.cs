using System.ComponentModel.DataAnnotations;

namespace carrinhoBack.Models
{
    public class ProdutoCompra
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdCompra { get; set; }
        public Guid IdProduto { get; set; }
        public int QuantidadeSelecionada { get; set; }
        public double Subtotal { get; set; }

    }
}
