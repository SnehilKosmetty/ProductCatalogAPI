using ProductCatalog.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<IEnumerable<ProductDto>> GetAvailableProductsAsync();
        Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
        Task<ProductDto> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto);
        Task<ProductDto> UpdateProductStockAsync(Guid id, UpdateStockDto updateStockDto);
        Task DeleteProductAsync(Guid id);
    }
}
