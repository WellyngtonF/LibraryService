using Api.Data;
using Api.Entities.DTOs;
using Api.Entities.Interfaces;
using Api.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class BookService : IBookService
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthorService _authorService;
    public BookService(ApplicationDbContext context, IAuthorService authorService)
    {
        _context = context;
        _authorService = authorService;
    }

    public async Task<Book> CreateBook(CreateBookDTO createBookDto)
    {
        var author = await _authorService.GetAuthorById(createBookDto.IDAuthor);
        if (author == null)
        {
            throw new ArgumentException("Invalid author ID");
        }

        var book = new Book
        {
            Title = createBookDto.Title,
            IDAuthor = createBookDto.IDAuthor,
            Description = createBookDto.Description,
            PagesRead = createBookDto.PagesRead,
            PagesTotal = createBookDto.PagesTotal,
            PublicationDate = createBookDto.PublicationDate
        };

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
