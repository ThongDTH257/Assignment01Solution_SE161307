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
        Member Login(string email, string password);
    }
}
