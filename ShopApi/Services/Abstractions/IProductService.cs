using ShopApi.Api.Dtos;
using ShopApi.Api.QueryParameters;

namespace ShopApi.Services.Abstractions;

public interface IProductService
{
    Task<PagedResult<ProductDto>> GetAllAsync(ProductQueryParameters parameters, CancellationToken ct);
    Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken ct);
}
