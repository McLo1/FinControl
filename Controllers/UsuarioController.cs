using FinControl.API.Models;
using FinControl.API.Services;
using Microsoft.AspNetCore.Mvc;
using FinControl.API.DTOs;
namespace FinControl.API.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly UsuarioService _Service;


        public UsuarioController(UsuarioService service)
        {
            _Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var UsuarioInfos = await _Service.GetAllAsync();
            return Ok(UsuarioInfos);
        }

        // [HttpGet("{id}")]
        [HttpGet("{id}", Name ="GetById")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var UsuariosById = await _Service.GetByIdAsync(id);
            if (UsuariosById == null) return NotFound($"Usuario com ID {id} Não encontrado");

            return Ok(UsuariosById);

        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateUsuarioDTO dto)
        {

            var novoUsuario = new Usuario
            {
                UserName = dto.Email,
                Email = dto.Email,
                Nome = dto.Nome
            
            };

            var resultado = await _Service.CreateAsync(novoUsuario, dto.Senha);

            if(!resultado.Succeeded) return BadRequest(resultado.Errors);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = novoUsuario.Id}, novoUsuario);


        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDTO dto)
        {
            var usuario = await _Service.GetByEmailAsync(dto.Email);
            if (usuario == null) return Unauthorized("E-mail ou senha incorretos");

            var ValidarSenha = await _Service.CheckPasswordAsync(usuario, dto.Senha);
            if (!ValidarSenha) return Unauthorized("E-mail ou senha incorretos");

            var token = _Service.GerarToken(usuario);

            return Ok(new
            {
                token,
                usuario = new
                {
                    id = usuario.Id,
                    nome = usuario.Nome,
                    email = usuario.Email,
                }

            });

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, UpdateUsuarioDTO dto)
        {
            var usuario = await _Service.GetByIdAsync(id);
            if (usuario == null) return NotFound($"Usuario com ID {id} Não encontrado");

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.UserName = dto.Email;
        
            var resultado = await _Service.UpdateAsync(usuario);

            if (!resultado.Succeeded) return BadRequest("Corpo da requisição incorreto");

            return Ok("Atualizado comm Sucesso");

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var usuario = await _Service.GetByIdAsync(id);
            if (usuario == null) return NotFound($"Usuario com ID {id} Não encontrado");


            var resultado = await _Service.DeleteAsync(usuario);

            if (!resultado.Succeeded) return BadRequest(resultado.Errors);

            return Ok("Deletado com sucesso");         
        }

        

    }
}