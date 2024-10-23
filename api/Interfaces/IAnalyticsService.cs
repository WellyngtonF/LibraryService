using Api.Entities;

namespace Api.Interfaces;

public interface IAnalyticsService
{    
    /// <summary>
    /// Calculates the total number of books for each author.
    /// </summary>
    /// <returns>A dictionary where the key is the author's ID and the value is the total number of books written by that author.</returns>
    Task<IDictionary<int, int>> GetTotalBooksByAuthor();
    
    /// <summary>
    /// Calculates the number of books read per month within a specified date range.
    /// </summary>
    /// <param name="startDate">The start date of the range to consider.</param>
    /// <param name="endDate">The end date of the range to consider.</param>
    /// <returns>A dictionary where the key is a date (representing a month) and the value is the number of books read in that month.</returns>
    Task<IDictionary<DateTime, int>> GetBooksReadPerMonth(DateTime startDate, DateTime endDate);
    
    /// <summary>
    /// Calculates the number of pages read per month within a specified date range.
    /// </summary>
    /// <param name="startDate">The start date of the range to consider.</param>
    /// <param name="endDate">The end date of the range to consider.</param>
    /// <returns>A dictionary where the key is a date (representing a month) and the value is the number of pages read in that month.</returns>
    Task<IDictionary<DateTime, int>> GetPagesReadPerMonth(DateTime startDate, DateTime endDate);
    
    /// <summary>
    /// Calculates the number of books purchased per month within a specified date range.
    /// </summary>
    /// <param name="startDate">The start date of the range to consider.</param>
    /// <param name="endDate">The end date of the range to consider.</param>
    /// <returns>A dictionary where the key is a date (representing a month) and the value is the number of books purchased in that month.</returns>
    Task<IDictionary<DateTime, int>> GetBooksPurchasedPerMonth(DateTime startDate, DateTime endDate);
    
    /// <summary>
    /// Calculates the average reading time.
    /// </summary>
    /// <returns>The average reading time in hours.</returns>
    Task<double> GetAverageReadingTime();
    
    /// <summary>
    /// Retrieves a list of unfinished books.
    /// </summary>
    /// <returns>A collection of unfinished books.</returns>
    Task<IEnumerable<Book>> GetUnfinishedBooks();
    
    /// <summary>
    /// Calculates the reading preference for each author.
    /// </summary>
    /// <returns>A dictionary where the key is the author's name and the value is the number of pages read by that author.</returns>
    Task<IDictionary<string, int>> GetAuthorReadingPreference();
}
