namespace Api.Entities.DTOs
{
    public class CreateBookDTO
    {
        public required string Title { get; set; }
        public required int IDAuthor { get; set; }
        public required string Description { get; set; }
        public int? PagesRead { get; set; }
        public int? PagesTotal { get; set; }
        public DateTime? PublicationDate { get; set; }
    }
}
