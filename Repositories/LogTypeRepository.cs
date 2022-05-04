using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newton_FTP_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newton_FTP_API.Repositories
{
    public class LogTypeRepository
    {
        private DataContext dataContext;
        public LogTypeRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<List<Models.LogType>> FindAll()
        {
            return await dataContext.LogTypes.ToListAsync();
        }

        public async Task<Models.LogType> FindById(int id)
        {
            return await dataContext.LogTypes.FindAsync(id);
        }
    }
}
