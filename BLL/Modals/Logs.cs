using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Modals
{
    public class Logs
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
        public string ExceptionMessage { get; set; }
        public string Exception { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string InnerException { get; set; }
        public System.Net.HttpStatusCode StatusCode { get; set; }
        public string UserID { get; set; }
    }
}
