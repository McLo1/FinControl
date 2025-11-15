using FinControl.API.Data;
using FinControl.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FinControl.API.Services
{
    public class CategoriasService
    {
        private readonly AppDbContext _Context;
        public CategoriasService(AppDbContext context)
        {
            _Context = context;
        }


        public async Task<List<Categoria>> GetAll()
        {
            return await _Context.Categorias.ToListAsync();
        }

        public async Task<Categoria?> GetById(int id)
        {
            return await _Context.Categorias.FindAsync(id);

        }

        public async Task<Categoria> Create(Categoria categoria)
        {
            _Context.Categorias.Add(categoria);
            await _Context.SaveChangesAsync();
            return categoria;
        }

        public async Task<bool> Update(Categoria categoria)
        {
            var existing = await _Context.Categorias.FindAsync(categoria.Id);
            if (existing == null) return false;

            existing.Nome = categoria.Nome;
            await _Context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> Delete(int id)
        {
            var Categoria = await _Context.Categorias.FindAsync(id);
            if (Categoria == null) return false;

            _Context.Categorias.Remove(Categoria);
            await _Context.SaveChangesAsync();

            return true; 
        }
    }
}