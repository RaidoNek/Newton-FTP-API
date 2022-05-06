using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newton_FTP_API.DAO
{
    public class FilterFTP
    {
        public int? Id { get; set; }
        public string Pattern { get; set; }
        public bool? isActive { get; set; }
        public string CreationUser { get; set; }
    }
}
