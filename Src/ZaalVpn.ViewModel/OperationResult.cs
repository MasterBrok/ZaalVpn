using System.Net;

namespace ZaalVpn.ViewModel;

public class OperationResult<T> : ResultModel
{
    public string[]? Messages { get; set; }
    public T? Data { get; set; } = default;
    public OperationResult<T> Succeeded(string? message = OperationMessage.Done)
    {
        Success = true;
        Messages = new string[] { message };
        return this;
    }

    public OperationResult<T> Failed(params string[] messages)
    {
        Success = false;
        Messages = messages;
        return this;
    }

    public OperationResult<T> Set(HttpStatusCode code)
    {
        HttpCode = code;
        return this;
    }
}

public class ApiResponse<A>
{
    public List<string> Messages { get; set; } = new();
    public A? Response { get; set; } = default;
    public bool Success { get; protected set; }
    public HttpStatusCode HttpCode { get; protected set; }


    public ApiResponse<A> Succeeded(string message = OperationMessage.Done)
    {
        HttpCode = HttpStatusCode.OK;
        Messages.Add(message);
        Success = true;
        return this;
    }

    public ApiResponse<A> Failed(string[] messages)
    {
        Success = false;
        return this;
    }

    public ApiResponse<A> FailedErrors(params string[] messages)
    {
        Success = false;
        return this;
    }
    public ApiResponse<A> Set(HttpStatusCode code)
    {
        HttpCode = code;
        return this;
    }

    public ApiResponse<A> NotFound()
    {
        Messages.Add(OperationMessage.NotFound);
        HttpCode = HttpStatusCode.NotFound;
        Success = false;
        return this;
    }
}