using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newton_FTP_API.DAO
{
    public class Log
    {
        public int? id { get; set; }
        public int? TypeId { get; set; }
        public int? FtpId { get; set; }
        public bool? Success { get; set; }
        public string Message { get; set; }
    }
}
