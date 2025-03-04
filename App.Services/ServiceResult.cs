using System.Net;

namespace Services;

public class ServiceResult<T>
{
    public ServiceResult()
    {
        ErrorMessages = new List<string>();
    }

    public T? Data { get; set; }
    public List<string>? ErrorMessages { get; set; }

    public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count == 0;

    public bool IsFailure => !IsSuccess;

    public HttpStatusCode StatusCode { get; set; }

    public static ServiceResult<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ServiceResult<T>
        {
            Data = data,
            StatusCode = statusCode
        };
    }

    public static ServiceResult<T> Failure(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>
        {
            ErrorMessages = [errorMessage],
            StatusCode = statusCode
        };
    }

    public static ServiceResult<T> Failure(List<string> errorMessages, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>
        {
            ErrorMessages = errorMessages,
            StatusCode = statusCode
        };
    }
}

public class ServiceResult
{
    public ServiceResult()
    {
        ErrorMessages = new List<string>();
    }

    public List<string>? ErrorMessages { get; set; }

    public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count == 0;

    public bool IsFailure => !IsSuccess;

    public HttpStatusCode StatusCode { get; set; }

    public static ServiceResult Success(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ServiceResult
        {
            StatusCode = statusCode
        };
    }

    public static ServiceResult Failure(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult
        {
            ErrorMessages = [errorMessage],
            StatusCode = statusCode
        };
    }

    public static ServiceResult Failure(List<string> errorMessages, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult
        {
            ErrorMessages = errorMessages,
            StatusCode = statusCode
        };
    }
}