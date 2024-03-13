using BookStore.BL.Interfaces;
using BookStore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Implementations
{
    public class TestService : ITestService
    {
        private readonly ApplicationDbContext _context;
        public TestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DoWork()
        {
                
        }
    }
}
