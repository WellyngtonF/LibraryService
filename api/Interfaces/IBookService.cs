using Api.DTOs;
using Api.Entities;

namespace Api.Interfaces;

public interface IBookService
{
    public Task<Book> GetBookById(int id);
    public Task<Book> CreateBook(CreateBookDTO createBookDto);
    public void UpdateBook(Book book);
    public void DeleteBook(int id);
    public Task<IEnumerable<Book>> GetBooks();
}
