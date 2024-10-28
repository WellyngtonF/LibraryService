using Api.Entities.Models;

namespace Api.Entities.Interfaces;

public interface IAnalyticsService
{    
    Task<IDictionary<int, int>> GetTotalBooksByAuthor();
    
    Task<IDictionary<DateTime, int>> GetBooksReadPerMonth(DateTime startDate, DateTime endDate);
    
    Task<IDictionary<DateTime, int>> GetPagesReadPerMonth(DateTime startDate, DateTime endDate);
    
    Task<IDictionary<DateTime, int>> GetBooksPurchasedPerMonth(DateTime startDate, DateTime endDate);
    
    Task<double> GetAverageReadingTime();
    
    Task<IEnumerable<Book>> GetUnfinishedBooks();
    
    Task<IDictionary<string, int>> GetAuthorReadingPreference();
}
