using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;


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
        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            _usuariosRepository.Cadastrar(usuario);
            return StatusCode(201);
        }
        [HttpGet("{id}")] // Faz a busca pelo ID.
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
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Usuario usuario)
        {
        _usuariosRepository.Atualizar(id, usuario);
        return StatusCode(204);
        }
        // delete -> /api/usuarios/{id}
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
