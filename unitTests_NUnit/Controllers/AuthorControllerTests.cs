using Api.Controllers;
using Api.Entities;
using Api.Exceptions;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace unitTests_NUnit.Controllers
{
    [TestFixture]
    public class AuthorControllerTests
    {
        private Mock<IAuthorService> _mockAuthorService;
        private AuthorController _controller;

        [SetUp]
        public void Setup()
        {
            _mockAuthorService = new Mock<IAuthorService>();
            _controller = new AuthorController(_mockAuthorService.Object);
        }

        [Test]
        public async Task GetAllAuthors_ReturnsOkResult()
        {
            // Arrange
            var authors = new List<Author> { new Author { Id = 1, Name = "Test Author" } };
            _mockAuthorService.Setup(service => service.GetAllAuthors()).ReturnsAsync(authors);

            // Act
            var result = await _controller.GetAllAuthors();

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(authors));
        }

        [Test]
        public async Task CreateAuthor_ValidAuthor_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var author = new Author { Name = "New Author" };
            _mockAuthorService.Setup(service => service.CreateAuthor(It.IsAny<Author>())).ReturnsAsync(author);

            // Act
            var result = await _controller.CreateAuthor(author);

            // Assert
            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.That(createdAtActionResult, Is.Not.Null);
            Assert.That(createdAtActionResult.ActionName, Is.EqualTo(nameof(AuthorController.GetAuthorById)));
            Assert.That(createdAtActionResult.Value, Is.EqualTo(author));
        }

        [Test]
        public async Task GetAuthorById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var author = new Author { Id = 1, Name = "Test Author" };
            _mockAuthorService.Setup(service => service.GetAuthorById(1)).ReturnsAsync(author);

            // Act
            var result = await _controller.GetAuthorById(1);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(author));
        }

        [Test]
        public async Task UpdateAuthor_ValidAuthor_ReturnsNoContent()
        {
            // Arrange
            var author = new Author { Id = 1, Name = "Updated Author" };
            _mockAuthorService.Setup(service => service.UpdateAuthor(It.IsAny<Author>()));

            // Act
            var result = await _controller.UpdateAuthor(author);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
            _mockAuthorService.Verify(service => service.UpdateAuthor(author), Times.Once);
        }

        [Test]
        public async Task DeleteAuthor_ExistingId_ReturnsNoContent()
        {
            // Arrange
            _mockAuthorService.Setup(service => service.DeleteAuthor(It.IsAny<int>()));

            // Act
            var result = await _controller.DeleteAuthor(1);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
            _mockAuthorService.Verify(service => service.DeleteAuthor(1), Times.Once);
        }

        // Error cases
        [Test]
        public async Task GetAuthorById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            _mockAuthorService.Setup(service => service.GetAuthorById(99)).ReturnsAsync((Author)null);

            // Act
            var result = await _controller.GetAuthorById(99);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.Null);
        }

        [Test]
        public void CreateAuthor_InvalidAuthor_ThrowsBadRequestException()
        {
            // Arrange
            Author nullAuthor = null;
            
            // Act & Assert
            Assert.ThrowsAsync<BadRequestException>(() => _controller.CreateAuthor(nullAuthor));
            _mockAuthorService.Verify(service => service.CreateAuthor(It.IsAny<Author>()), Times.Never);
        }

        [Test]
        public void CreateAuthor_ServiceThrowsBadRequestException_ThrowsBadRequestException()
        {
            // Arrange
            var author = new Author { Name = "Test" };
            _mockAuthorService
                .Setup(service => service.CreateAuthor(It.IsAny<Author>()))
                .ThrowsAsync(new BadRequestException("Test exception"));

            // Act & Assert
            Assert.ThrowsAsync<BadRequestException>(() => _controller.CreateAuthor(author));
        }
    }
}
