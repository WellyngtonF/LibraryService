using Api.Entities;
using Api.Interfaces;

namespace Api.Services;

public class AnalyticsService : IAnalyticsService
{
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;

    public AnalyticsService(IBookService bookService, IAuthorService authorService)
    {
        _bookService = bookService;
        _authorService = authorService;
    }

    public async Task<IDictionary<string, int>> GetAuthorReadingPreference()
    {
        var books = await _bookService.GetBooks();
        var authors = await _authorService.GetAllAuthors();
        var authorReadingPreferences = books.GroupBy(book => authors.First(author => author.Id == book.IDAuthor).Name)
                                             .ToDictionary(group => group.Key, group => group.Sum(book => book.PagesRead ?? 0));
        return authorReadingPreferences;
    }

    public async Task<double> GetAverageReadingTime()
    {
        var books = await _bookService.GetBooks();
        var totalReadingTime = books.Where(book => book.StartDate.HasValue && book.EndDate.HasValue)
                                    .Sum(book => (book.EndDate.Value - book.StartDate.Value).TotalHours);
        return totalReadingTime / books.Count();
    }

    public async Task<IDictionary<DateTime, int>> GetBooksPurchasedPerMonth(DateTime startDate, DateTime endDate)
    {
        var books = await _bookService.GetBooks();
        return books.Where(book => book.PurchaseDate.HasValue
                                && book.PurchaseDate.Value >= startDate && book.PurchaseDate.Value <= endDate)
                    .GroupBy(book => new DateTime(book.PurchaseDate.Value.Year, book.PurchaseDate.Value.Month, 1))
                    .ToDictionary(group => group.Key, group => group.Count());
    }

    public async Task<IDictionary<DateTime, int>> GetBooksReadPerMonth(DateTime startDate, DateTime endDate)
    {
        var books = await _bookService.GetBooks();
        return books.Where(book => book.StartDate.HasValue && book.EndDate.HasValue
                                && book.StartDate.Value >= startDate && book.EndDate.Value <= endDate)
                    .GroupBy(book => new DateTime(book.StartDate.Value.Year, book.StartDate.Value.Month, 1))
                    .ToDictionary(group => group.Key, group => group.Count());
    }

    public async Task<IDictionary<DateTime, int>> GetPagesReadPerMonth(DateTime startDate, DateTime endDate)
    {
        var books = await _bookService.GetBooks();
        return books.Where(book => book.StartDate.HasValue
                                && book.StartDate.Value >= startDate && book.StartDate.Value <= endDate)
                    .GroupBy(book => new DateTime(book.StartDate.Value.Year, book.StartDate.Value.Month, 1))
                    .ToDictionary(
                        group => group.Key,
                        group => group.Sum(book => book.PagesRead ?? 0)
                    );
    }

    public async Task<IDictionary<int, int>> GetTotalBooksByAuthor()
    {
        var books = await _bookService.GetBooks();
        return books.GroupBy(book => book.IDAuthor)
                   .ToDictionary(group => group.Key, group => group.Count());
    }

    public async Task<IEnumerable<Book>> GetUnfinishedBooks()
    {
        var books = await _bookService.GetBooks();
        return books.Where(book => book.EndDate == null);
    }
}
