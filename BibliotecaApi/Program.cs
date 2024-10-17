using Api.Middleware;
using Aplicacao.Queries;
using Aplicacao.Queries.Assuntos;
using Core.Repositorios;
using Infra;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions( x =>
{
    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
} );

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped( typeof( IRepositorioBase<> ), typeof( RepositorioBase<> ) );

builder.Services.AddScoped<BibliotecaContexto>();
builder.Services.AddDbContext<BibliotecaContexto>(
    options => options.UseSqlServer( "name=ConnectionStrings:DefaultConnection" ) );

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddMediatR( cfg => cfg.RegisterServicesFromAssemblyContaining<BuscarAssuntoPorCodigoQuery>() );

builder.Services.AddCors( options =>
{
    options.AddPolicy( name: "BibliotecaCors",
                      policy =>
                      {
                          policy.WithOrigins( "http://localhost:4200" )
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      } );
} );



var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<BibliotecaContexto>();
context.Database.EnsureCreated();

if( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseExceptionHandler();
app.UseCors( "BibliotecaCors" );

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
