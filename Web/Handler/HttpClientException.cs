using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Handler
{
    public class HttpClientException : Exception
    {
        public BLL.Modals.Logs Logs { get; set; }
        public HttpClientException(string message) : base(message)
        {
        }
        public HttpClientException(string message, BLL.Modals.Logs log) : base(message)
        {
            this.Logs = log;
        }
    }
}
