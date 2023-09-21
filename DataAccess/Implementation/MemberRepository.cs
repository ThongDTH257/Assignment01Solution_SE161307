using BusinessObject.Models;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        private readonly FstoreDbContext context;
        public MemberRepository(FstoreDbContext context) : base(context) 
        {
            this.context = context;
        }

        public Member GetByEmail(string email)
        {
            var member = context.Members.FirstOrDefault(e => e.Email == email);
            return member;
        }

        public async  Task<Member> Login(string email, string password)
        {
            var member = await context.Members
                                .FirstOrDefaultAsync(m => m.Email == email && m.Password == password);
            return member;
        }
    }
}
