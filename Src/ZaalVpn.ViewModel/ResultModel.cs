using System.Net;
using System.Text.Json.Serialization;

namespace ZaalVpn.ViewModel;

public class ResultModel
{
    public ResultModel(bool success, HttpStatusCode httpCode)
    {
        Success = success;
        Messages = new();
        HttpCode = httpCode;

    }

    public ResultModel(bool success)
    {
        Success = success;
    }

    public ResultModel()
    {

    }
    public bool Success { get; set; }
    public List<string> Messages { get; set; } = new();
    public HttpStatusCode HttpCode { get; set; }


    public ResultModel Succeeded(params string[]? messages)
    {
        HttpCode = HttpStatusCode.OK;
        Success = true;
        Messages.AddRange(messages);
        return this;
    }

    public ResultModel Failed(params string[] messages)
    {
        Success = false;
        Messages.AddRange(messages);
        return this;
    }

    public ResultModel Set(HttpStatusCode code)
    {
        HttpCode = code;
        return this;
    }

    //public static explicit operator ResultViewModel(IdentityResult result)
    //{
    //    return new BaseResult(result.Succeeded)
    //    {
    //        Messages = result.Errors.Select(e => $"{e.Description} || {e.Code}").ToList(),
    //        HttpCode = result.Succeeded ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest
    //    };
    //}
    public ResultModel NotFound()
    {
        HttpCode = HttpStatusCode.NotFound;
        Messages.Add(OperationMessage.NotFound);
        Success = false;
        return this;
    }
    public ResultModel OK()
    {
        HttpCode = HttpStatusCode.OK;
        Messages.Add(OperationMessage.Done);
        Success = true;
        return this;
    }

    public ResultModel FailedLogin()
    {
        HttpCode = HttpStatusCode.NotFound;
        Messages.Add(OperationMessage.NotFound);
        Messages.Add(OperationMessage.PUIncorrect);
        Success = false;
        return this;
    }
}