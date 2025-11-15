using FinControl.API.Models;
using FinControl.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinControl.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriasService _Services;

        public CategoriaController(CategoriasService service)
        {
            _Services = service;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAll()
        {
            var Categorias = await _Services.GetAll();
            return Ok(Categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetID(int id)
        {
            var CategoriaItem = await _Services.GetById(id);
            if (CategoriaItem == null) return NotFound();

            return Ok(CategoriaItem);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> Create([FromBody] Categoria categoria)
        {
            var CategoriaItem = await _Services.Create(categoria);
            return CreatedAtAction(nameof(GetID), new { id = CategoriaItem.Id }, CategoriaItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Categoria categoria)
        {
            if (id != categoria.Id) return NotFound();

            var CategoriaAtualizada = await _Services.Update(categoria);

            if (!CategoriaAtualizada) return NotFound();
            return Ok("Atualizado com Sucesso");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var CategoriaDeletada = await _Services.Delete(id);
            if (!CategoriaDeletada) return NotFound("Não encontrado");
            return Ok("Deletado com Sucesso");
        }

    }
}