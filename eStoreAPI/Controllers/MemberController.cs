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
        [HttpGet("{id}")]
        public ActionResult<Member> GetMember(int id) => unitOfWork.Member.GetById(id);
        [HttpGet("Email/{email}")]
        public ActionResult<Member> CheckEmail(string email) => unitOfWork.Member.GetByEmail(email);
        [HttpPost("Login")]
        public async Task<ActionResult<Member>> Login([FromBody]LoginModel model)
        {
            var member  = new Member();
            member = await unitOfWork.Member.Login(model.Email, model.Password);
            if(member == null)
            {
                return NotFound("Invalid email or password!");
            }
            return Ok(member);
        }
        [HttpPost]
        public ActionResult Create([FromBody]MemberCreate memberCreate)
        {
            Random random = new Random();
            int id = random.Next(6, 10000);
            var member = new Member
            {
                Email = memberCreate.Email,
                Password = memberCreate.Password,
                City = memberCreate.City,
                CompanyName = memberCreate.CompanyName,
                Country = memberCreate.Country,

            };
            var result = unitOfWork.Member.Create(member);
            return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult UpdateMember(int id, MemberCreate memberCreate)
        {
            var member = unitOfWork.Member.GetById(id);
            member.Email = memberCreate.Email;
            member.City = memberCreate.City;
            member.CompanyName = memberCreate.CompanyName;
            member.Country = memberCreate.Country;
            if(memberCreate.Password!= null && memberCreate.Password != member.Password)
            {
                member.Password = memberCreate.Password;
            }
            unitOfWork.Member.Update(member);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id) 
        {
            unitOfWork.Member.Delete(id);
            return NoContent();
        }
    }
}
