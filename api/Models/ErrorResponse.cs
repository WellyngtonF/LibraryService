namespace Api.Models;

public class ErrorResponse
{
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public string? StackTrace { get; set; }

    public ErrorResponse(string message, int statusCode, string? stackTrace = null)
    {
        Message = message;
        StatusCode = statusCode;
        StackTrace = stackTrace;
    }
}
