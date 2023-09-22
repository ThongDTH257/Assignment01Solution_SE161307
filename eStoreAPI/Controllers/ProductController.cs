using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public ActionResult GetAllProduct()
        {
            var products = unitOfWork.Product.GetAll();
            return Ok(products);
        }
        [HttpGet("Category")]
        public ActionResult GetProductWithCategory() 
        {
            var products = unitOfWork.Product.GetFullInfo();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id) => unitOfWork.Product.GetById(id); 

        [HttpGet("Search/{keyword}")]
        public ActionResult<List<Product>> SearchProduct(string keyword)
        {
                var products = unitOfWork.Product.FilterSearch(keyword, null, null);
                return Ok(products);   
        }
        [HttpPost]
        public ActionResult Create([FromBody] ProductCreateDTO productDTO)
        {
            Random random = new Random();
            int id = random.Next(6, 10000);
            var product = new Product
            {
                ProductId = id,
                ProductName = productDTO.ProductName,
                UnitPrice = productDTO.UnitPrice,
                UnitsInStock = productDTO.UnitsInStock,
                CategoryId = productDTO.CategoryId,
                Weight = productDTO.Weight,
            };
            var result = unitOfWork.Product.Create(product); 
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductView productView)
        {
            var product = unitOfWork.Product.GetById(id);
            if (product == null) return NotFound();
            product.UnitPrice = productView.UnitPrice;
            product.CategoryId = productView.CategoryId;
            product.Weight = productView.Weight;
            product.ProductName = productView.ProductName;
            product.UnitsInStock = productView.UnitsInStock;
            unitOfWork.Product.Update(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id) 
        {
            var product = unitOfWork.Product.GetById(id);
            if (product == null) return NotFound();
            unitOfWork.Product.Delete(product);
            return NoContent();
        }
    }
}
