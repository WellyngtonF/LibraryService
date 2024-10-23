using Api.Interfaces;
using LibraryService.Data;
using LibraryService.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class BookService : IBookService
{
    private readonly ApplicationDbContext _context;

    public BookService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Book> CreateBook(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async void DeleteBook(int id)
    {
        var book = await GetBookById(id);
        if (book == null)
        {
            return;
        }
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }

    public async Task<Book> GetBookById(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task<IEnumerable<Book>> GetBooks()
    {
        return await _context.Books.ToListAsync();
    }

    public async void UpdateBook(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }
}
