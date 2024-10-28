using NUnit.Framework;
using Api.Entities.Models;

namespace unitTests_NUnit.Entities
{
    [TestFixture]
    public class BookTests
    {
        [Test]
        public void Book_Properties_SetAndGetCorrectly()
        {
            // Arrange
            var book = new Book
            {
                Id = 1,
                Title = "Test Book",
                IDAuthor = 1,
                Description = "Test Description",
                PagesRead = 50,
                PagesTotal = 200,
                PublicationDate = new DateTime(2023, 1, 1),
                PurchaseDate = new DateTime(2023, 2, 1),
                StartDate = new DateTime(2023, 3, 1),
                EndDate = new DateTime(2023, 4, 1)
            };

            // Act & Assert
            Assert.That(book.Id, Is.EqualTo(1));
            Assert.That(book.Title, Is.EqualTo("Test Book"));
            Assert.That(book.IDAuthor, Is.EqualTo(1));
            Assert.That(book.Description, Is.EqualTo("Test Description"));
            Assert.That(book.PagesRead, Is.EqualTo(50));
            Assert.That(book.PagesTotal, Is.EqualTo(200));
            Assert.That(book.PublicationDate, Is.EqualTo(new DateTime(2023, 1, 1)));
            Assert.That(book.PurchaseDate, Is.EqualTo(new DateTime(2023, 2, 1)));
            Assert.That(book.StartDate, Is.EqualTo(new DateTime(2023, 3, 1)));
            Assert.That(book.EndDate, Is.EqualTo(new DateTime(2023, 4, 1)));
        }
    }
}

