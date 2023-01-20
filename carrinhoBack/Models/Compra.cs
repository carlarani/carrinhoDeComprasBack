
using System.ComponentModel.DataAnnotations;

namespace carrinhoBack.Models
{
    public class Compra
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdComprador { get; set; }

        public double ValorTotal { get; set; }

        public string Status { get; set; }

        public Compra(Guid id, Guid idComprador,  double ValorTotal)
        {
            this.Id = id;
            this.IdComprador = idComprador;
            this.ValorTotal = ValorTotal;
            this.Status = "Rascunho";
        }


    }
}
