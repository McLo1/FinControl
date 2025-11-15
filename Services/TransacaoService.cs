using FinControl.API.Data;
using FinControl.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinControl.API.Services
{
    public class TransacaoService
    {

        private readonly AppDbContext _Context;

        public TransacaoService(AppDbContext context)
        {
            _Context = context;
        }


        public async Task<ActionResult<List<Transacao>>> GetAllAsync()
        {
            return await _Context.Transacoes
            .Include(t => t.Categoria)
            .Include(t => t.Usuario)
            .ToListAsync();

        }

        public async Task<Transacao?> GetAsyncById(int id)
        {
            return await _Context.Transacoes.FindAsync(id);
        }

        public async Task<Transacao> CreateAsync(Transacao transacao)
        {
            _Context.Transacoes.Add(transacao);
            await _Context.SaveChangesAsync();
            return transacao;
        }

        public async Task<bool> UpdateAsync(Transacao transacao)
        {
            var transacaoItem = await _Context.Transacoes.FindAsync(transacao.Id);
            if (transacaoItem == null) return false;

            transacaoItem.Descricao = transacao.Descricao;
            transacaoItem.Valor = transacao.Valor;
            transacaoItem.Data = transacao.Data;
            transacaoItem.UsuarioId = transacao.UsuarioId;

            _Context.Transacoes.Update(transacaoItem);
            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var transacaoItem = await _Context.Transacoes.FindAsync(id);
            if (transacaoItem == null) return false;


            _Context.Transacoes.Remove(transacaoItem);
            await _Context.SaveChangesAsync();
            return true;
        }

    }
}