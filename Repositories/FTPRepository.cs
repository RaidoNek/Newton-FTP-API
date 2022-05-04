using Microsoft.EntityFrameworkCore;
using Newton_FTP_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newton_FTP_API.Repositories
{
    public class FTPRepository
    {
        private DataContext dataContext;
        public FTPRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<Models.FTP> FindById(int id)
        {
            return await dataContext.FTPs.FindAsync(id);
        }

        public async Task<List<Models.FTP>> GetAllFTPs()
        {
            return await dataContext.FTPs.ToListAsync();
        }
    }
}
