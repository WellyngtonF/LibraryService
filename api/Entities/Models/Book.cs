using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        [ForeignKey("Author")]
        public required int IDAuthor { get; set; }
        public Author? Author { get; set; }
        public required string Description { get; set; }
        public int? PagesRead { get; set; }
        public int? PagesTotal { get; set; }
        public DateTime? PublicationDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
