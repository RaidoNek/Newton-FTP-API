using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Newton_FTP_API.DAO {
    public class FTP
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string LoginHost { get; set; }
        public string LoginName { get; set; }
        public string LoginPass { get; set; }
        public string Directory { get; set; }
        public TimeSpan DaysBackwards { get; set; }
        public string Pattern { get; set; }
        public DateTime ActiveSince { get; set; }
        public DateTime ActiveTo { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationUser { get; set; }
    }
}
