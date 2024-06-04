using BookStore.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly string? _connectionString;

        public DbSet<BookType> BookTypes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole { Id = Guid.Parse("af3c4ceb-2c5f-4b07-abcc-e2cd010de7f5").ToString(), Name = "Moderator", NormalizedName = "MODERATOR" },
                    new IdentityRole { Id = Guid.Parse("58ff34ee-ed69-4d43-9124-53ac00c07c85").ToString(), Name = "Admin", NormalizedName = "ADMIN" }
                );
        }
    }
}
