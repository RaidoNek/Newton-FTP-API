using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Newton_FTP_API.Models
{
    [Table("Log")]
    public class Log
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int FtpId { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationUser { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
