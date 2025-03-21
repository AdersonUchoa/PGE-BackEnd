using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Application.InterfaceResponseHandler
{
    public interface ISuccessStatus<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Body { get; set; }
    }
}
