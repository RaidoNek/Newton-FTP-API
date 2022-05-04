using Newton_FTP_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newton_FTP_API.Operations
{
    public class FTPOperation
    {
        private FTPRepository ftpRepository;
        public FTPOperation(FTPRepository ftpRepository)
        {
            this.ftpRepository = ftpRepository;
        }

        public async Task<List<Models.FTP>> GetAllFTPs()
        {
            var FTPs = await ftpRepository.GetAllFTPs();
            return FTPs;
        }
    }
}
