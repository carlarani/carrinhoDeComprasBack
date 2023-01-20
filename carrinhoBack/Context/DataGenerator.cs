using carrinhoBack.Models;
using System.Text.Json;

namespace carrinhoBack.Context
{
    public class DataGenerator
    {
        private readonly CarrinhoContext _carrinhoContext;

        public DataGenerator(CarrinhoContext carrinhoContext)
        {
            _carrinhoContext = carrinhoContext;
        }

        public void Generate()
        {

            if (!_carrinhoContext.Usuarios.Any())
            {
                List<Usuario> usuarios;
                using (StreamReader r = new StreamReader("user.json"))
                {
                    string json = r.ReadToEnd();
                    usuarios = JsonSerializer.Deserialize<List<Usuario>>(json);
                }

                _carrinhoContext.Usuarios.AddRange(usuarios);
                _carrinhoContext.SaveChanges();
            }

            if (!_carrinhoContext.Produtos.Any())
            {
                List<Produto> produtos;
                using (StreamReader r = new StreamReader("product.json"))
                {
                    string json = r.ReadToEnd();
                    produtos = JsonSerializer.Deserialize<List<Produto>>(json);
                }

                _carrinhoContext.Produtos.AddRange(produtos);
                _carrinhoContext.SaveChanges();
            }
        }
    }
}
