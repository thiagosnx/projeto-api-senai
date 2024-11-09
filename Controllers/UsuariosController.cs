using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;



namespace Exo.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _usuariosRepository;
        public UsuariosController(UsuarioRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_usuariosRepository.Listar());
        }
        // [HttpPost]
        // public IActionResult Cadastrar(Usuario usuario)
        // {
        //     _usuariosRepository.Cadastrar(usuario);
        //     return StatusCode(201);
        // }
        public IActionResult Post(Usuario usuario)
{
Usuario usuarioBuscado = _usuariosRepository.Login(usuario.Email,
usuario.Senha);
if (usuarioBuscado == null)
{
return NotFound("E-mail ou senha inválidos!");
}
// Se o usuário for encontrado, segue a criação do token.
// Define os dados que serão fornecidos no token - Payload.
var claims = new[]
{
// Armazena na claim o e-mail usuário autenticado.
new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
// Armazena na claim o id do usuário autenticado.
new Claim(JwtRegisteredClaimNames.Jti,
usuarioBuscado.Id.ToString()),
};
var key = new
SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chaveautenticacao"));
var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
var token = new JwtSecurityToken(
issuer: "exoapi.webapi", 
audience: "exoapi.webapi", 
claims: claims, 
expires: DateTime.Now.AddMinutes(30), 
signingCredentials: creds 
);
return Ok(
new { token = new JwtSecurityTokenHandler().WriteToken(token) }
);
}
        [HttpGet("{id}")] 
        public IActionResult BuscarPorId(int id)
        {
        Usuario usuario = _usuariosRepository.BuscaPorId(id);
        if (usuario == null)
        {
        return NotFound();
        }
        return Ok(usuario);
        }
        // put -> /api/usuarios/{id}
        // Atualiza.
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Usuario usuario)
        {
        _usuariosRepository.Atualizar(id, usuario);
        return StatusCode(204);
        }
        // delete -> /api/usuarios/{id}
        [Authorize]

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
        try
        {
        _usuariosRepository.Deletar(id);
        return StatusCode(204);
        }
        catch (Exception e)
        {
        return BadRequest();
        }
        }

    }
}
