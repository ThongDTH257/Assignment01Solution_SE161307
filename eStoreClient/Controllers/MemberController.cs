using BusinessObject.DTO;
using BusinessObject.Models;
using eStoreClient.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http.Headers;

namespace eStoreClient.Controllers
{
    public class MemberController : Controller
    {
        private readonly HttpClient client = null;
        private string memberApiUrl = "";
        public MemberController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            memberApiUrl = "https://localhost:7150/api/Member";
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string role = HttpContext.Session.GetString("ROLE");
            if(role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }else if (role != "admin")
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Member");
            }
            List<Member> members = await ApiHandler.DeserializeApiResponse<List<Member>>(memberApiUrl, HttpMethod.Get);
            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }
            return View(members);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MemberCreate memberCreate)
        {
            Member member = await ApiHandler.DeserializeApiResponse<Member>(memberApiUrl + "/Email/" + memberCreate.Email, HttpMethod.Get);
            if(member != null)
            {
                TempData["ErrorMessage"] = "Email already exists.";
                return RedirectToAction("Create");
            }
            await ApiHandler.DeserializeApiResponse(memberApiUrl, HttpMethod.Post, memberCreate);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
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

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
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

            Member member = await ApiHandler.DeserializeApiResponse<Member>(memberApiUrl + "/" + id, HttpMethod.Get);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(member);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MemberCreate memberCreate)
        {
            await ApiHandler.DeserializeApiResponse(memberApiUrl + "/" + memberCreate.Id, HttpMethod.Put, memberCreate);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
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
            Member member = await ApiHandler.DeserializeApiResponse<Member>(memberApiUrl + "/" + id, HttpMethod.Get);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(member);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(MemberCreate memberCreate)
        {
            HttpResponseMessage response = await client.DeleteAsync(memberApiUrl + "/" + memberCreate.Id);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return View();
        }

    }
}
