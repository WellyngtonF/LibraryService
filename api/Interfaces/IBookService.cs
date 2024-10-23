using LibraryService.Entities;

namespace Api.Interfaces;

public interface IBookService
{
    public Task<Book> GetBookById(int id);
    public Task<Book> CreateBook(Book book);
    public void UpdateBook(Book book);
    public void DeleteBook(int id);
    public Task<IEnumerable<Book>> GetBooks();
}
