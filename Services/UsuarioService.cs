using FinControl.API.Data;
using FinControl.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinControl.API.Services
{
    public class UsuarioService
    {
        private readonly UserManager<Usuario> _usermanager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IConfiguration _Config;

        public UsuarioService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IConfiguration config)
        {
            _usermanager = userManager;
            _signInManager = signInManager;
            _Config = config;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _usermanager.Users.ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(string id)
        {
            return await _usermanager.FindByIdAsync(id);
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _usermanager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> CreateAsync(Usuario usuario, string senha)
        {
            return await _usermanager.CreateAsync(usuario, senha);
        }

        public async Task<IdentityResult> UpdateAsync(Usuario usuario)
        {
            return await _usermanager.UpdateAsync(usuario);
        }

        public async Task<IdentityResult> DeleteAsync(Usuario usuario)
        {
            return await _usermanager.DeleteAsync(usuario);
        }

        public async Task<Usuario?> FindByEmailAsync(string email)
        {
            return await _usermanager.FindByEmailAsync(email);
        }

        public async Task<bool> CheckPasswordAsync(Usuario usuario, string senha)
        {
            return await _usermanager.CheckPasswordAsync(usuario, senha);
        }

        public string GerarToken(Usuario usuario)
        {
            var jwtSettings = _Config.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            var claims = new List<Claim>
            {
                new Claim (JwtRegisteredClaimNames.Sub, usuario.Id),
                new Claim (JwtRegisteredClaimNames.Email, usuario.Email ?? ""),
                new Claim("nome", usuario.Nome ?? "")
            };

            var creds = new SigningCredentials(
                new SymmetricSecurityKey(key),
                 SecurityAlgorithms.HmacSha256

            );

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}