using BusinessObject.DTO;
using BusinessObject.Models;
using eStoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eStoreClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client = null;
        private string memberApiUrl = "";

        public HomeController(ILogger<HomeController> logger)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            memberApiUrl = "https://localhost:7150/api/Member";
            _logger = logger;
        }

        public IActionResult Index()
        {
            string role = HttpContext.Session.GetString("ROLE");
            if(role== "admin")
                return RedirectToAction("Privacy");
            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }
            return View();
        }
        public async Task<IActionResult> Index(LoginModel loginModel)
        {
            HttpResponseMessage response = await client.GetAsync(memberApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
            Member admin = new Member
            {
                Email = config["Credentials:Email"],
                Password = config["Credentials:Password"],
            };
            List<Member> members = JsonSerializer.Deserialize<List<Member>>(stringData, options);
            members.Add(admin);
            Member account = members.Where(e=>e.Email == loginModel.Email && e.Password==loginModel.Password).FirstOrDefault();
            if (account != null)
            {
                HttpContext.Session.SetInt32("USERID", account.MemberId);
                HttpContext.Session.SetString("EMAIL", account.Email);
                if (account.Email == "admin@estore.com")
                {
                    HttpContext.Session.SetString("ROLE", "admin");
                    return RedirectToAction("Privacy", "Home");
                }
                else
                {
                    HttpContext.Session.SetString("ROLE", "member");
                    return RedirectToAction("Privacy", "Home");
                }
            }
            return View();  
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}