using BusinessObject.Models;
using DataAccess.Interface;
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
    }
}
