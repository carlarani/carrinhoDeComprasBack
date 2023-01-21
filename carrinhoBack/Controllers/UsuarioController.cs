using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using carrinhoBack.Models;
using carrinhoBack.Auth;
using NuGet.Protocol.Core.Types;
using carrinhoBack.Auth;
using carrinhoBack.Repository;
using Microsoft.AspNetCore.Authorization;
using carrinhoBack.Context;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace carrinhoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly CarrinhoContext _context;
        private readonly TokenService _tokenService;
        private readonly UsuarioRepository _usuarioRepository;


        public UsuarioController(CarrinhoContext context, TokenService generateToken, UsuarioRepository usuarioRepository)
        {
            _context = context;
            _tokenService = generateToken;
            _usuarioRepository = usuarioRepository;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .OrderBy(x => x.Nome)
                .ToListAsync();
            //foreach(var usuario in usuarios)
            //{
            //    usuario.Senha = "";
            //}
            return Ok(usuarios);
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]

        public async Task<ActionResult<Usuario>> GetUsuario(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]

        public async Task<IActionResult> PutUsuario(Guid id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        //login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UsuarioAuth usuarioAuth)
        {
            var user = await _usuarioRepository.GetUser(usuarioAuth.Email, usuarioAuth.Senha);
            if (user == null)
                return NotFound(new { message = "Usuário ou senha Inválidos" });

            var token = _tokenService.GenerateToken(user);
            user.Senha = "";
            return Ok(new
            {
                user = user.Id.ToString(),
                token = token, 
            });
        }

        //valida
        [HttpPost]
        [Route("valida")]
        public async Task<IActionResult> Valida([FromHeader]string token)
        {
            if(token == null)
            {
                return NotFound();
            }

            JwtSecurityToken tokenValidado = _tokenService.Validate(token);

            if(tokenValidado==null)
            {
                return BadRequest();
            }
            else
            {
                var usuarioRole = tokenValidado.Claims.First(x => x.Type == "role").Value;
                
                return Ok(new {
                    role= usuarioRole,
                });
            }

        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(Guid id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

    }
}
