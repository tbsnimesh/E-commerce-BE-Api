using Microsoft.EntityFrameworkCore;
using ShopApi.Api.Dtos;
using ShopApi.Api.Mapping;
using ShopApi.Api.QueryParameters;
using ShopApi.Data;
using ShopApi.Domain.Entities;
using ShopApi.Services.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ShopApi.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _db;

    public ProductService(AppDbContext db)
    {
        _db = db;
    }

    public async  Task<PagedResult<ProductDto>> GetAllAsync(ProductQueryParameters parameters, CancellationToken ct)
    {
        IQueryable<Product> baseQuery = _db.Products
        .AsNoTracking()
        .Include(p => p.Category);
        
         if (!string.IsNullOrWhiteSpace(parameters.Search))
        {
            var term = parameters.Search.Trim();
            baseQuery = baseQuery.Where(p => p.Name.Contains(term));
        }

        // --- Count AFTER filtering, BEFORE paging ---
        var totalCount = await baseQuery.CountAsync(ct);


        // --- Sorting ---
        baseQuery = ApplySort(baseQuery, parameters);

        // --- Paging ---
        var products = await baseQuery
            .Skip(parameters.Skip)
            .Take(parameters.PageSize)
            .ToListAsync(ct);

        var items = products.Select(p => p.ToDto()).ToList();

        return new PagedResult<ProductDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = parameters.Page,
            PageSize = parameters.PageSize
        };





        /*  var products = await _db.Products   
                          .AsNoTracking()
                          //Include says: "when you load each Product, also JOIN to its Category and populate Product.Category."
                          .Include(p => p.Category)
                          .OrderBy(p => p.Name)
                          .ToListAsync(ct);
          //LINQ. For each Product p in the list, call p.ToDto() (the extension method from the mapper) and produce a new sequence of Product Dtos..ToList() turn that sequence into a concrete List<ProductDto>.
          //Why call .ToList() after Select? Because Select is lazy � it returns an IEnumerable that hasn't actually run yet. If you returned that directly, the mapping would happen later, possibly multiple times. .ToList() forces it to run once, here.This is a common pattern to transform entities to DTOs after fetching them from the database.
          return products.Select(p => p.ToDto()).ToList();*/
    }

    private static IQueryable<Product> ApplySort(
    IQueryable<Product> source,
    ProductQueryParameters query)
    {
        var desc = query.IsDescending;

        return query.SortBy?.ToLowerInvariant() switch
        {
            "price" => desc ? source.OrderByDescending(p => p.Price)
                            : source.OrderBy(p => p.Price),
            "name" => desc ? source.OrderByDescending(p => p.Name)
                            : source.OrderBy(p => p.Name),
            _ => source.OrderBy(p => p.Name)   // default sort
        };
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var product = await _db.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id, ct);

        //if it is null , return null. Otherwise, call ToDto() to convert the Product entity to a ProductDto and return that.
        return product?.ToDto();
    }
}
