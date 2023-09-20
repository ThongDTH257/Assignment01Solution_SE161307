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
        [HttpGet("category")]
        public ActionResult GetProductWithCategory() 
        {
            var products = unitOfWork.Product.GetFullInfo();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id) => unitOfWork.Product.GetById(id); 

        [HttpGet("Search")]
        public ActionResult SearchProduct([FromQuery] ProductFilter filter)
        {
                var products = unitOfWork.Product.FilterSearch(filter.Name, filter.MinPrice, filter.MaxPrice);
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
    }
}
