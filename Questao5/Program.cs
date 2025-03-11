using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Questao5.Application.Validators;
using Questao5.Infrastructure;
using Questao5.Infrastructure.Providers;
using Questao5.Infrastructure.Providers.Customizations;
using Questao5.Infrastructure.Sqlite;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options => options.ErrorCustomization());

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.EnableAnnotations());

//Fluent Validation
builder.Services.AddValidatorsFromAssemblyContaining<MovimentoCcCommandValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddLocalization();
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.ConfigureLocalization();

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

app.Run();

// Informações úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html


