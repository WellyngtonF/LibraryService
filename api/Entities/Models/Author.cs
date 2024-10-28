using System.ComponentModel.DataAnnotations;

namespace Api.Entities.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}