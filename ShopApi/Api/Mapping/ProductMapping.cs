using ShopApi.Api.Dtos;
using ShopApi.Domain.Entities;

namespace ShopApi.Api.Mapping;

public static class ProductMapping
{
    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto(
            Id: product.Id,
            Name: product.Name,
            Description: product.Description,
            Price: product.Price,
            StockQuantity: product.StockQuantity,
            ImageUrl: product.ImageUrl,
            CategoryId: product.CategoryId,
            CategoryName: product.Category?.Name ?? string.Empty
        );
    }
}