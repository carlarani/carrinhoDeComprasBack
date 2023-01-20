using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace carrinhoBack.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }


        public Usuario(Guid id, string nome, string email, string senha)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.Role = "comprador";
        }
        public Usuario(string nome, string email, string senha)
        {
            this.Id = new Guid();
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.Role = "comprador";
        }
        public Usuario() { }

    }
}
