

using CashSmart.Aplicacao;
using CashSmart.Aplicacao.Interface;
using CashSmart.Repositorio;
using CashSmart.Repositorio.Contexto;
using CashSmart.Repositorio.Contratos;
using CashSmart.Servicos.Services.Criptografia;
using CashSmart.Servicos.Services.Criptografia.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


builder.Services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();


// criptografia 
builder.Services.AddScoped<IBcryptSenhaService, BcryptSenhaService >();
builder.Services.AddDbContext<CashSmartContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CashSmartContexto")));


builder.Services.AddCors(options => {
    options.AddDefaultPolicy(builder => {
        builder.WithOrigins("http://localhost:5173")
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .AllowAnyHeader()
        .AllowAnyMethod();
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

app.UseAuthorization();

app.MapControllers();

app.Run();