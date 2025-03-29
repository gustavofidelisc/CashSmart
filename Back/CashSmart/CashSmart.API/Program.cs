

using System.Text;
using CashSmart.Aplicacao;
using CashSmart.Aplicacao.Interface;
using CashSmart.Dominio.Shared;
using CashSmart.Repositorio;
using CashSmart.Repositorio.Contexto;
using CashSmart.Repositorio.Contratos;
using CashSmart.Servicos.Services.Criptografia;
using CashSmart.Servicos.Services.Criptografia.Interface;
using CashSmart.Servicos.Services.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Name;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.Configure<JwtConfiguracoes>(builder.Configuration.GetSection("Jwt"));


builder.Services.AddDbContext<CashSmartContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CashSmartContexto")));


builder.Services.AddAuthentication( x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    
    }).AddJwtBearer(x => {
        x.TokenValidationParameters = new TokenValidationParameters{
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });
builder.Services.AddAuthorization();


builder.Services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

builder.Services.AddScoped<IParcelaRepositorio, ParcelaRepositorio>();


builder.Services.AddScoped<IFormaPagamentoRepositorio, FormaPagamentoRepositorio>();
builder.Services.AddScoped<IFormaPagamentoAplicacao, FormaPagamentoAplicacao>();

builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<ICategoriaAplicacao, CategoriaAplicacao>();

builder.Services.AddScoped<ITransacaoRepositorio, TransacaoRepositorio>();


builder.Services.AddScoped<ITiposTransacaoAplicacao, TiposTransacaoAplicacao>();

// jwt
builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();

// criptografia 
builder.Services.AddTransient<IBcryptSenhaService, BcryptSenhaService >();

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(builder => {
        builder.WithOrigins("http://localhost:5173")
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


// Adicionar o Swagger e configurar a autenticação
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor insira o token JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();