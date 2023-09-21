    using Microsoft.AspNetCore.Mvc;

namespace eStoreClient.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
