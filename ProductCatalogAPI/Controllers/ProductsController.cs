using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>A list of all products</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Gets all available products (in stock)
        /// </summary>
        /// <returns>A list of available products</returns>
        [HttpGet("available")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAvailable()
        {
            var products = await _productService.GetAvailableProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Gets a specific product by id
        /// </summary>
        /// <param name="id">The product id</param>
        /// <returns>The product if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="createProductDto">The product data</param>
        /// <returns>The created product</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDto>> Create(CreateProductDto createProductDto)
        {
            var product = await _productService.CreateProductAsync(createProductDto);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id">The product id</param>
        /// <param name="updateProductDto">The updated product data</param>
        /// <returns>The updated product</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDto>> Update(Guid id, UpdateProductDto updateProductDto)
        {
            var product = await _productService.UpdateProductAsync(id, updateProductDto);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        /// <summary>
        /// Updates the stock of an existing product
        /// </summary>
        /// <param name="id">The product id</param>
        /// <param name="updateStockDto">The updated stock data</param>
        /// <returns>The updated product</returns>
        [HttpPatch("{id}/stock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDto>> UpdateStock(Guid id, UpdateStockDto updateStockDto)
        {
            var product = await _productService.UpdateProductStockAsync(id, updateStockDto);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        /// <summary>
        /// Deletes a product
        /// </summary>
        /// <param name="id">The product id</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
