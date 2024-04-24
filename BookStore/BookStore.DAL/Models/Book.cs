using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace BookStore.DAL.Models
{
    public class Book
    {
        [Key]
        public Guid ID { get; set; }
        [DisplayName("Titlu:")]
        [Required(ErrorMessage = "Campul 'Titlu' este obligatoriu!")]
        public string? Title { get; set; }
        [DisplayName("Descriere:")]
        [Required(ErrorMessage = "Campul 'Descriere' este obligatoriu!")]
        public string? Description { get; set; }
        [DisplayName("Autor:")]
        [Required(ErrorMessage = "Campul 'Autor' este obligatoriu!")]
        public string? Author { get; set; }
        [DisplayName("Editura:")]
        [Required(ErrorMessage = "Campul 'Editura' este obligatoriu!")]
        public string? Editura { get; set; }
        [DisplayName("Pret:")]
        [Required(ErrorMessage = "You need to insert the price!")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public float Price { get; set; }
        public string? Path { get; set; }
        public int BookLikes { get; set; } = 0;
        [NotMapped]
        [DisplayName("Book cover:")]
        public IFormFile? FormFile { get; set; }
        public string? PublicID { get; set; }


        [DisplayName("Book type:")]
        public Guid BookTypeId { get; set; }
        public BookType? BookType { get; set; }
    }
}
