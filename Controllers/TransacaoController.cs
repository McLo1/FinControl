using FinControl.API.Models;
using FinControl.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinControl.API.Controllers
{
    
    public class TransacaoController : ControllerBase
    {
        private readonly TransacaoService _Service;

        public TransacaoController(TransacaoService service)
        {
            _Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transacao>>> GetAllAsync()
        {
            var transacoes = await _Service.GetAllAsync();
            return Ok(transacoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transacao>> GetByIdAsync(int id)
        {
            var transacoes = await _Service.GetAsyncById(id);
            return Ok(transacoes);
        }

        [HttpPost]
        public async Task<ActionResult<Transacao>> CreateAsync(Transacao transacao)
        {
            var transacoes = await _Service.CreateAsync(transacao);

            return Ok(transacoes);
        }

        [HttpPut]
        public async Task<ActionResult<Transacao>> UpdateAsync(Transacao transacao)
        {

            var Transacao = await _Service.GetAsyncById(transacao.Id);
            if (Transacao == null) NotFound("Não encontrado");

            if (!ModelState.IsValid) return BadRequest("Corpo da Requisição incorreto");       

            return Ok(Transacao);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Transacao>> DeleteAsync(int id)
        {

            var Transacao = await _Service.GetAsyncById(id);
            if (Transacao == null) NotFound("Não encontrado");
            if (!ModelState.IsValid) return BadRequest("Corpo da Requisição incorreto");       


            await _Service.DeleteAsync(id);

            return Ok(Transacao);
        }

    }

}