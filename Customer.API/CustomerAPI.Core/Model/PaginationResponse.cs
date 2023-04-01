using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Core.Model
{
    public class PaginationResponse<T>
    {
        public int PerPage { get; set; }
        public int LastPage { get; set; }
        public int CurrentPage { get; set; }
        public int? Total { get; set; }
        public IEnumerable<T>? Data { get; set; }
    }
}
