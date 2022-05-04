using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newton_FTP_API.DTO
{
    public class Log
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string FTPName { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationUser { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
