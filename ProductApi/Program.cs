using ProductApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProductServices();
builder.Services.AddProductHandlers();
builder.Services.AddProductServices();
builder.Services.AddProductValidators();
builder.Services.AddProductRepositoryServices();
builder.Services.AddProductApplicationServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.AddGlobalErrorHandler();

app.Run();