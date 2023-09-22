using BusinessObject.DTO;
using BusinessObject.Models;
using eStoreClient.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace eStoreClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient client = null;
        private string productApiUrl = "";
        private string categoryApiUrl = "";
        public ProductController() 
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            productApiUrl = "https://localhost:7150/api/Product";
            categoryApiUrl = "https://localhost:7150/api/Category";
        }
        public async Task<IActionResult> Index()
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (role != "admin")
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Member");
            }
            List<Product> products = await ApiHandler.DeserializeApiResponse<List<Product>>(productApiUrl + "/Category", HttpMethod.Get);
            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }
            return View(products);
        }

        public async Task<IActionResult> Search(string keyword)
        {
            ViewData["keyword"] = keyword;
            List<Product> products = await ApiHandler.DeserializeApiResponse<List<Product>>(productApiUrl + "/Search/"+keyword, HttpMethod.Get);
            return View("Index", products);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (role != "admin")
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Member");
            }
            List<Category> categories = await ApiHandler.DeserializeApiResponse<List<Category>>(categoryApiUrl, HttpMethod.Get);
            ViewData["Categories"] = categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDTO product)
        {
            await ApiHandler.DeserializeApiResponse(productApiUrl , HttpMethod.Post, product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Product product = await ApiHandler.DeserializeApiResponse<Product>(productApiUrl + "/" + id, HttpMethod.Get);
            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            HttpResponseMessage response = await client.DeleteAsync(productApiUrl + "/" + product.ProductId);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return View();
        }
    }
}
