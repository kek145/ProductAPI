using System;
using AutoMapper;
using System.Linq;
using Product.DAL.Data;
using Product.Domain.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Product.Domain.Helpers;

namespace Product.DAL.Repositories.ProductRepository;

public class ProductRepository : IProductRepository
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public ProductRepository(IMapper mapper, ApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task DeleteProductAsync(int productId)
    {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == productId);

        if (product is null)
            throw new Exception("Product not found!");

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public IQueryable<ProductDto> GetAllProducts()
    {
        return _context.Products
            .AsNoTracking()
            .AsQueryable().ProjectTo<ProductDto>(_mapper.ConfigurationProvider);
    }

    public async Task AddProductAsync(ProductDto product)
    {
        var map = _mapper.Map<Domain.DbSet.Product>(product);

        await _context.Products.AddAsync(map);
        await _context.SaveChangesAsync();
        
        product.Id = map.Id;
    }

    public async Task UpdateProductAsync(ProductDto product)
    {
        var productMap = _mapper.Map<Domain.DbSet.Product>(product);
        
        _context.Products.Update(productMap);
        
        await _context.SaveChangesAsync();
    }

    public async Task<ProductDto> GetProductByIdAsync(int productId)
    {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == productId);

        if (product is null)
            throw new Exception("Product not found!");

        var result = _mapper.Map<ProductDto>(product);
        
        return result;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _context.Products
            .AsNoTracking()
            .ToListAsync();

        var result = _mapper.Map<IEnumerable<ProductDto>>(products);

        return result;
    }

    public async Task<PagedResult<ProductDto>> GetAllProductAsync<TResult>(QueryParameters queryParameters)
    {
        if (queryParameters.PageNumber < 1)
        {
            queryParameters.PageNumber = 1;
        }
        
        var totalSize = await _context.Products.CountAsync();
        
        var startIndex = (queryParameters.PageNumber - 1) * queryParameters.PageSize;
        
        var totalPages = (int)Math.Ceiling((double)totalSize / queryParameters.PageSize);
        
        var items = await _context.Products.Skip(startIndex)
            .AsSplitQuery()
            .AsNoTracking()
            .Take(queryParameters.PageSize)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync();


        return new PagedResult<ProductDto>
        {
            Items = items,
            PageNumber = queryParameters.PageNumber,
            RecordNumber = queryParameters.PageSize,
            TotalCount = totalSize,
            TotalPages = totalPages
        };
    }
}