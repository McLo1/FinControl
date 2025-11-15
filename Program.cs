using System.Text;
using FinControl.API.Data;
using FinControl.API.Models;
using FinControl.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

//Faz o Build
var builder = WebApplication.CreateBuilder(args);

//Gerencia a conexão com MySQL
var ConnStr = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseMySql(ConnStr, ServerVersion.AutoDetect(ConnStr)));

//Configura o Identity
builder.Services.AddIdentityCore<Usuario>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>()
.AddSignInManager<SignInManager<Usuario>>();

//Configura a autenticação com JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");
var Key = Encoding.UTF8.GetBytes(jwtSettings["key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
.AddJwtBearer(Options =>
{
    Options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});


//Adiciona os serviços ao contêiner de injeção de dependência
builder.Services.AddScoped<CategoriasService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();



var app = builder.Build();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();


