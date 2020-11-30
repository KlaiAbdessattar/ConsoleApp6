using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp6.Utils
{
    public class ApiResponse
    {
        public object data { get; set; }
        public object metaData { get; set; }

        public ApiResponse(object data)
        {
            this.data = data;
            this.metaData = new object();
        }
        public ApiResponse(object data, object metadata)
        {
            this.data = data;
            this.metaData = metadata;
        }
    }
}
