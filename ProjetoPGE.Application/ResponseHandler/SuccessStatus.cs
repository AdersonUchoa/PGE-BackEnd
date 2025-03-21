using Microsoft.AspNetCore.Mvc;
using ProjetoPGE.Application.InterfaceResponseHandler;

namespace ProjetoPGE.Application.ResponseHandler
{
    public class SuccessStatus<T> : ObjectResult, ISuccessStatus<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Body { get; set; }
        public SuccessStatus(bool success, string message, T body) : base(new { success, message, body })
        {
            Success = success;
            Message = message;
            Body = body;
            StatusCode = 200;
        }
    }
}
