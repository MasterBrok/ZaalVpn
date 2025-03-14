﻿using System.Net;

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
    public bool Success { get; protected set; }
    public List<string> Messages { get; set; } = new();
    public HttpStatusCode HttpCode { get; protected set; }


    public ResultModel Succeeded(params string[]? messages)
    {
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
}