using AutoMapper;
using Newton_FTP_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newton_FTP_API.Repositories;
using AutoMapper.QueryableExtensions;

namespace Newton_FTP_API.Operations
{
    public class LogOperation
    {
        private IMapper mapper;
        private LogRepository logRepository;
        private FTPRepository ftpRepository;
        private LogTypeRepository logTypeRepository;
        public LogOperation(IMapper mapper, LogRepository logRepository, FTPRepository ftpRepository, LogTypeRepository logTypeRepository)
        {
            this.mapper = mapper;
            this.logRepository = logRepository;
            this.ftpRepository = ftpRepository;
            this.logTypeRepository = logTypeRepository;
        }

        public async Task<List<Models.Log>> GetAll(DAO.FilterLog filter)
        {
            return await logRepository.GetAndFilter(filter);
        }

        public async Task DeleteLog(int id)
        {
            await logRepository.DeleteLog(id);
        }
        private async Task<DTO.Log> MapLog(Models.Log log)
        {
            var Type = await logTypeRepository.FindById(log.TypeId);
            var FTP = await ftpRepository.FindById(log.FtpId);

            var LogDTO = mapper.Map<DTO.Log>(log);
            LogDTO.TypeName = Type.Name;
            LogDTO.FTPName = FTP.Name;

            return LogDTO;
        }
        public async Task<DTO.Log> Map(Models.Log log)
        {
            return await MapLog(log);
        }

        public async Task<List<DTO.Log>> MapAll(List<Models.Log> logs)
        {
            List<DTO.Log> l = new List<DTO.Log>();
            //l.AsQueryable().ProjectTo<DTO.Log>(this.mapper.ConfigurationProvider);

            foreach (var log in logs)
                l.Add(await MapLog(log));

            return l;
        }

        public async Task<Models.Log> AddLog(DAO.Log log)
        {
            var Log = await logRepository.AddLog(log);
            return Log;
        }

        public async Task<Models.Log> UpdateLog(DAO.Log targetLog)
        {
            return await logRepository.Update(await logRepository.FindById((int)targetLog.id), targetLog);
        }
    }
}
