using Api.DTOs;
using Api.Entities;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet("author-reading-preference")]
    public async Task<IDictionary<string, int>> GetAuthorReadingPreference()
    {
        return await _analyticsService.GetAuthorReadingPreference();
    }

    [HttpGet("average-reading-time")]
    public async Task<double> GetAverageReadingTime()
    {
        return await _analyticsService.GetAverageReadingTime();
    }

    [HttpGet("books-purchased-per-month")]
    public async Task<IDictionary<DateTime, int>> GetBooksPurchasedPerMonth(DateTime startDate, DateTime endDate)
    {
        return await _analyticsService.GetBooksPurchasedPerMonth(startDate, endDate);
    }

    [HttpGet("books-read-per-month")]
    public async Task<IDictionary<DateTime, int>> GetBooksReadPerMonth(DateTime startDate, DateTime endDate)
    {
        return await _analyticsService.GetBooksReadPerMonth(startDate, endDate);
    }

    [HttpGet("pages-read-per-month")]
    public async Task<IDictionary<DateTime, int>> GetPagesReadPerMonth(DateTime startDate, DateTime endDate)
    {
        return await _analyticsService.GetPagesReadPerMonth(startDate, endDate);
    }

    [HttpGet("total-books-per-author")]
    public async Task<IDictionary<int, int>> GetTotalBooksByAuthor()
    {
        return await _analyticsService.GetTotalBooksByAuthor();
    }

    [HttpGet("unfinished-books")]
    public async Task<IEnumerable<Book>> GetUnfinishedBooks()
    {
        return await _analyticsService.GetUnfinishedBooks();
    }

}