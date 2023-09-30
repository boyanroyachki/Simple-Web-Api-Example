using ApiTraining.Data;
using ApiTraining.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiTraining.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
                this.productService = productService;  
        }

        /// <summary>
        /// Get a list with all products.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/products
        ///     {
        /// 
        ///     }
        /// </remarks>
        ///<returns>Returns "OK" with a list full of products.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts() 
        {
            return this.productService.GetAllProducts();
        }

        /// <summary>
        /// Gets a product by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/products/{id}
        ///     {
        ///     
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id) 
        {
            var product = this.productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/products
        ///     {
        ///         "name": "Ice cream",
        ///         "Description": "Tasty!"
        ///     }
        /// </remarks>>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Product> PostProduct(Product product)
        {
            product = this.productService
                .CreateProduct(product.Name, product.Description);

            return CreatedAtAction("PostProduct", product);
        }
        [HttpPut("{id}")]
        public ActionResult PutProduct(int id, Product product)
        {
            if(id != product.Id) return BadRequest();
            if (this.productService.GetById(id) == null) return NotFound();

            this.productService.EditProduct(id, product);
            return NoContent();
        }
        [HttpPatch("{id}")]
        public ActionResult PatchProduct(int id, Product product)
        {
            if (this.productService.GetById(id) == null) return NotFound();
            this.productService.EditProductPartially(id, product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> DeleteProduct(int id) 
        {
            if (this.productService.GetById(id) == null) return NotFound();

            var deletedProduct = this.productService.DeleteProduct(id);

            return Ok(deletedProduct);

        }
    }
}
