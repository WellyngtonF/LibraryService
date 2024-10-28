using Api.Controllers;
using Api.Entities.DTOs;
using Api.Entities.Models;
using Api.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace unitTests_NUnit.Controllers
{
    [TestFixture]
    public class BookControllerTests
    {
        private Mock<IBookService> _mockBookService;
        private BookController _controller;

        [SetUp]
        public void Setup()
        {
            _mockBookService = new Mock<IBookService>();
            _controller = new BookController(_mockBookService.Object);
        }

        [Test]
        public async Task GetBooks_ReturnsListOfBooks()
        {
            // Arrange
            var books = new List<Book> { new Book { Id = 1, Title = "Test Book", IDAuthor = 1, Description = "Test Description" } };
            _mockBookService.Setup(service => service.GetBooks()).ReturnsAsync(books);

            // Act
            var result = await _controller.GetBooks();

            // Assert
            Assert.That(result, Is.EqualTo(books));
        }

        [Test]
        public async Task CreateBook_ValidBook_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var createBookDto = new CreateBookDTO { Title = "New Book", IDAuthor = 1, Description = "Test Description" };
            var createdBook = new Book { Id = 1, Title = createBookDto.Title, IDAuthor = createBookDto.IDAuthor, Description = createBookDto.Description };
            _mockBookService.Setup(service => service.CreateBook(It.IsAny<CreateBookDTO>())).ReturnsAsync(createdBook);

            // Act
            var result = await _controller.CreateBook(createBookDto);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.That(createdAtActionResult.ActionName, Is.EqualTo(nameof(BookController.GetBookById)));
            Assert.That(createdAtActionResult.Value, Is.EqualTo(createdBook));
        }

        [Test]
        public async Task GetBookById_ExistingId_ReturnsBook()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book", IDAuthor = 1, Description = "Test Description" };
            _mockBookService.Setup(service => service.GetBookById(1)).ReturnsAsync(book);

            // Act
            var result = await _controller.GetBookById(1);

            // Assert
            Assert.That(result, Is.EqualTo(book));
        }

        [Test]
        public async Task UpdateBook_ValidBook_CallsService()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Updated Book", IDAuthor = 1, Description = "Updated Description" };
            _mockBookService.Setup(service => service.UpdateBook(It.IsAny<Book>()));

            // Act
            _controller.UpdateBook(book);

            // Assert
            _mockBookService.Verify(service => service.UpdateBook(book), Times.Once);
        }

        [Test]
        public async Task DeleteBook_ExistingId_CallsService()
        {
            // Arrange
            _mockBookService.Setup(service => service.DeleteBook(It.IsAny<int>()));

            // Act
            _controller.DeleteBook(1);

            // Assert
            _mockBookService.Verify(service => service.DeleteBook(1), Times.Once);
        }

        // Error cases
        [Test]
        public async Task GetBookById_NonExistingId_ReturnsNull()
        {
            // Arrange
            _mockBookService.Setup(service => service.GetBookById(99)).ReturnsAsync((Book)null);

            // Act
            var result = await _controller.GetBookById(99);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task CreateBook_ServiceThrowsException_PropagatesException()
        {
            // Arrange
            var createBookDto = new CreateBookDTO { Title = "Test Book", IDAuthor = 1, Description = "Test Description" };
            _mockBookService
                .Setup(service => service.CreateBook(It.IsAny<CreateBookDTO>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _controller.CreateBook(createBookDto));
        }
    }
}
