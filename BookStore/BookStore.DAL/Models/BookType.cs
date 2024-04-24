using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.DAL.Models
{
    public class BookType
    {
        [Key]
        public Guid BookTypeID { get; set; }
        [JsonIgnore]
        public string? PublicId { get; set; }
        [DisplayName("Nume tip:")]
        [Required(ErrorMessage = "Campul este obligatoriu!")]
        public string TypeName { get; set; }
        [DisplayName("Descriere tip:")]
        [Required(ErrorMessage = "Campul este obligatoriu!")]
        public string TypeDescription { get; set; }
    }
}
