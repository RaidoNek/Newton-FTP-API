using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newton_FTP_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Providers.Entities;

namespace Newton_FTP_API.Repositories
{
    public class LogRepository
    {
        private DataContext dataContext;
        private IMapper mapper;
        public LogRepository(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<List<Models.Log>> GetAndFilter(DAO.FilterLog filter)
        {
            List<Models.Log> logs = await dataContext.Logs
                .Where(l => filter.TypeId == null || l.TypeId == filter.TypeId)
                .Where(l => filter.FtpId == null || l.FtpId == filter.FtpId)
                .Where(l => filter.Success == null || l.Success == filter.Success)
                .Where(l => filter.Message == null || l.Message == filter.Message)
                .ToListAsync();
            return logs;

        }

        public async Task<Models.Log> FindById(int id)
        {
            return await dataContext.Logs.FindAsync(id);
        }

        public async Task<Models.Log> AddLog(DAO.Log log)
        {
            var Log = mapper.Map<Models.Log>(log);
            Log.CreationDate = DateTime.Now;
            Log.CreationUser = "AD USER";

            dataContext.Logs.Add(Log);
            await dataContext.SaveChangesAsync();
            return Log;
        }

        public async Task DeleteLog(int id)
        {
            Models.Log log = await FindById(id);
            dataContext.Logs.Remove(log);
            await dataContext.SaveChangesAsync();
        }
        public async Task<Models.Log> Update(Models.Log log, DAO.Log targetLog)
        {
            log.TypeId = (int)targetLog.TypeId;
            log.FtpId = (int)targetLog.FtpId;
            log.Success = (bool)targetLog.Success;
            log.Message = targetLog.Message;

            dataContext.Entry(log).State = EntityState.Modified;

            await dataContext.SaveChangesAsync();
            return log;
        }
    }
}
