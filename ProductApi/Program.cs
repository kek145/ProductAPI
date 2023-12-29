using System;
using MediatR;
using Product.DAL.Data;
using Product.Domain.Responses;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Product.BLL.Services.ProductService;
using Microsoft.Extensions.DependencyInjection;
using Product.DAL.Repositories.ProductRepository;
using Product.BLL.Queries.ProductQueries.GetById;
using Product.BLL.Commands.ProductCommands.Create;
using Product.BLL.Commands.ProductCommands.Delete;
using Product.BLL.Commands.ProductCommands.Update;
using Product.BLL.Queries.ProductQueries.GetAllProducts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddTransient<IRequestHandler<UpdateProductCommand>, UpdateProductCommandHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteProductCommand>, DeleteProductCommandHandler>();
builder.Services.AddTransient<IRequestHandler<CreateProductCommand, int>, CreateProductCommandHandler>();
builder.Services.AddTransient<IRequestHandler<GetProductByIdQuery, ProductResponse>, GetProductByIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponse>>, GetAllProductsQueryHandler>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseSnakeCaseNamingConvention();
});


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(Program).Assembly));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();