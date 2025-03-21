using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Application.DTOs
{
    public class PagedResultDTO<T>
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public List<T> Items { get; set; }

        public PagedResultDTO(int total, int page, int size, List<T> items)
        {
            Total = total;
            Page = page;
            Size = size;
            Items = items;
        }
    }
}
