using NUnit.Framework;
using Api.Entities.Models;

namespace unitTests_NUnit.Entities
{
    [TestFixture]
    public class AuthorTests
    {
        [Test]
        public void Author_Properties_SetAndGetCorrectly()
        {
            // Arrange
            var author = new Author
            {
                Id = 1,
                Name = "Test Author"
            };

            // Act & Assert
            Assert.That(author.Id, Is.EqualTo(1));
            Assert.That(author.Name, Is.EqualTo("Test Author"));
        }
    }
}

