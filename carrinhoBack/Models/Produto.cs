using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace carrinhoBack.Models
{
    public class Produto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        [JsonPropertyName("preco")]
        public double Preco { get; set; }
        [JsonPropertyName("quantidade")]
        public int Estoque { get; set; }
        [JsonPropertyName("idVendedor")]
        public int? idVendedor { get; set; }


    }
}
