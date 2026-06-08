using Microsoft.AspNetCore.Http.HttpResults;
using ShopApi.Api.Dtos;
using ShopApi.Api.QueryParameters;
using ShopApi.Services.Abstractions;

namespace ShopApi.Api.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products")
                       .WithTags("Products");

        group.MapGet("/", async (
            [AsParameters] ProductQueryParameters parameters,
            IProductService productService,
            CancellationToken ct) =>
        {
            var result = await productService.GetAllAsync(parameters, ct);
            return TypedResults.Ok(result);
        })
        .WithName("GetAllProducts");

        group.MapGet("/{id:guid}", async Task<Results<Ok<ProductDto>, NotFound>> (
            Guid id,
            IProductService productService,
            CancellationToken ct) =>
        {
            var product = await productService.GetByIdAsync(id, ct);

            return product is null
                ? TypedResults.NotFound()
                : TypedResults.Ok(product);
        })
        .WithName("GetProductById");
    }
}
