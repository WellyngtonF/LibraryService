using System.ComponentModel.DataAnnotations;

namespace LibraryService.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Description { get; set; }
        public DateTime? PublicationDate { get; set; }
    }
}