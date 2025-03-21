using Microsoft.AspNetCore.Mvc;
using ProjetoPGE.Application.InterfaceResponseHandler;

namespace ProjetoPGE.Application.ResponseHandler
{
    public class InternalServerErrorStatus<T> : ObjectResult, IInternalServerErrorStatus<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public InternalServerErrorStatus(bool success, string message, string error) : base(new { success, message, error })
        {
            Success = success;
            Message = message;
            Error = error;
            StatusCode = 500;
        }
    }
}
