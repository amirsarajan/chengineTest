using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topsales.Infrastructure
{
    public class Response<TResult>
    {
        public TResult Content { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
        public int ItemsPerPage { get; set; }
        public int StatusCode { get; set; }
        public string RequestId { get; set; }
        public string LongId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string[]> ValidationErrors { get; set; }
    }

}
