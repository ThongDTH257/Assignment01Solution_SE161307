using BusinessObject.Models;
using DataAccess.Implementation;
using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FstoreDbContext context;
        public UnitOfWork(FstoreDbContext context) 
        {
            this.context = context;
            Member = new MemberRepository(context);
        }
        public IMemberRepository Member { get; private set; }

        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
