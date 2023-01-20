using carrinhoBack.Context;
using carrinhoBack.Models;

namespace carrinhoBack.Repository
{
    public class UsuarioRepository
    {
            private readonly CarrinhoContext _carrinhoContext;

            public UsuarioRepository(CarrinhoContext carrinhoContext)
            {
                _carrinhoContext = carrinhoContext;
            }


            public Task<Usuario?> GetUser(string email, string senha)
            {
                return Task.Run(() =>
                {
                    var user = _carrinhoContext.Usuarios
                    .FirstOrDefault(item => item.Email.Equals(email) && item.Senha.Equals(senha));
                    return user;
                });
            }
            public Task<Usuario> CreateUser(Usuario usuario)
            {
                return Task.Run(() =>
                {
                    _carrinhoContext.Add(usuario);
                    _carrinhoContext.SaveChanges();
                    return usuario;
                });
            }
        }
    }
