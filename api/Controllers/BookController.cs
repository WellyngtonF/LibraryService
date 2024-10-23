using Api.Interfaces;
using LibraryService.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IEnumerable<Book>> GetBooks()
    {
        return await _bookService.GetBooks();
    }

    [HttpGet("{id}")]
    public async Task<Book> GetBookById(int id)
    {
        return await _bookService.GetBookById(id);
    }

    [HttpPost]
    public async Task<Book> CreateBook(Book book)
    {
        return await _bookService.CreateBook(book);
    }

    [HttpPut]
    public void UpdateBook(Book book)
    {
        _bookService.UpdateBook(book);
    }

    [HttpDelete("{id}")]
    public void DeleteBook(int id)
    {
        _bookService.DeleteBook(id);
    }
}
