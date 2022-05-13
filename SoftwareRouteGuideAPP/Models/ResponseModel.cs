using System;
using System.Collections.Generic;

namespace SoftwareRouteGuideAPP.Models
{
    public class ResponseModel<T>
    {
        public ResponseModel()
        {
            
        }

        public T data { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
    }
}