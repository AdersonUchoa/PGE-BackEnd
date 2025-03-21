using Microsoft.AspNetCore.Mvc;
using ProjetoPGE.Application.InterfaceResponseHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Application.ResponseHandler
{
    public class BadRequestStatus<T> : ObjectResult, IBadRequestStatus<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public BadRequestStatus(bool success, string message, string error) : base(new { success, message, error })
        {
            Success = success;
            Message = message;
            Error = error;
            StatusCode = 400;
        }
    }
}
