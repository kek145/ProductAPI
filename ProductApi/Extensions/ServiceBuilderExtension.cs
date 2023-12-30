using System;
using MediatR;
using FluentValidation;
using Product.Domain.Requests;
using Product.Domain.Responses;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Product.BLL.Validators.Product;
using Product.BLL.Services.ProductService;
using Microsoft.Extensions.DependencyInjection;
using Product.BLL.Queries.ProductQueries.GetById;
using Product.DAL.Repositories.ProductRepository;
using Product.BLL.Commands.ProductCommands.Create;
using Product.BLL.Commands.ProductCommands.Delete;
using Product.BLL.Commands.ProductCommands.Update;
using Product.BLL.Queries.ProductQueries.GetAllProducts;
using Product.DAL.Data;
using Product.Domain.Helpers;

namespace ProductApi.Extensions;

public static class ServiceBuilderExtension
{
    public static IServiceCollection AddProductRepositoryServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        return serviceCollection;
    }

    public static IServiceCollection AddProductServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IProductService, ProductService>();
        return serviceCollection;
    }

    public static IServiceCollection AddProductHandlers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IRequestHandler<UpdateProductCommand>, UpdateProductCommandHandler>();
        serviceCollection.AddTransient<IRequestHandler<DeleteProductCommand>, DeleteProductCommandHandler>();
        serviceCollection.AddTransient<IRequestHandler<CreateProductCommand, int>, CreateProductCommandHandler>();
        serviceCollection.AddTransient<IRequestHandler<GetProductByIdQuery, ProductResponse>, GetProductByIdQueryHandler>();
        serviceCollection.AddTransient<IRequestHandler<GetAllProductsQuery, PagedResult<ProductResponse>>, GetAllProductsQueryHandler>();
        
        return serviceCollection;
    }

    public static IServiceCollection AddProductValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IValidator<ProductRequest>, ProductRequestValidator>();
        return serviceCollection;
    }

    public static IServiceCollection AddProductApplicationServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention();
        });


        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        serviceCollection.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(Program).Assembly));
        
        return serviceCollection;
    }
}