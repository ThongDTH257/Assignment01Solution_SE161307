using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public MemberController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public ActionResult GetMembers()
        {
            var members = unitOfWork.Member.GetAll();
            return Ok(members);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<Member>> Login([FromBody]LoginModel model)
        {
            var member = await unitOfWork.Member.Login(model.Email, model.Password);
            if(member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }
        [HttpPost("Register")]
        public ActionResult Register([FromBody]MemberCreate memberCreate)
        {
            Random random = new Random();
            int id = random.Next(6, 10000);
            var member = new Member
            {
                MemberId = id,
                Email = memberCreate.Email,
                Password = memberCreate.Password,
                City = memberCreate.City,
                CompanyName = memberCreate.CompanyName,
                Country = memberCreate.Country,

            };
            var result = unitOfWork.Member.Create(member);
            return Ok(result);
        }
    }
}
