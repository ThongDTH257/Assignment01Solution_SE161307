using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        Task<Member> Login(string email, string password);
        Member GetByEmail(string email);
    }
}
