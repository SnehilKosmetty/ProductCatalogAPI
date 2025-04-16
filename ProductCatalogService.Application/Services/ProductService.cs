using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Domain.Interfaces;
using ProductCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return null;

            return MapToDto(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(MapToDto);
        }

        public async Task<IEnumerable<ProductDto>> GetAvailableProductsAsync()
        {
            var products = await _productRepository.GetAvailableAsync();
            return products.Select(MapToDto);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = new Product(
                createProductDto.Name,
                createProductDto.Description,
                createProductDto.Price,
                createProductDto.StockQuantity
            );

            await _productRepository.AddAsync(product);
            return MapToDto(product);
        }

        public async Task<ProductDto> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return null;

            product.UpdateDetails(
                updateProductDto.Name,
                updateProductDto.Description,
                updateProductDto.Price
            );

            await _productRepository.UpdateAsync(product);
            return MapToDto(product);
        }

        public async Task<ProductDto> UpdateProductStockAsync(Guid id, UpdateStockDto updateStockDto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return null;

            product.UpdateStock(updateStockDto.StockQuantity);

            await _productRepository.UpdateAsync(product);
            return MapToDto(product);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }

        private static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                IsAvailable = product.IsAvailable,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
    }
}
