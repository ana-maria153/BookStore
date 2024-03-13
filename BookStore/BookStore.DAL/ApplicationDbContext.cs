using BookStore.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly string? _connectionString;

        public DbSet<Book> Books { get; set; }

        public ApplicationDbContext(string? connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder opBuilder)
        {
            opBuilder.UseSqlServer(_connectionString);
        }
    }
}
