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
            Models.LogType Type = await logTypeRepository.FindById(log.TypeId);
            Models.FTP FTP = await ftpRepository.FindById(log.FtpId);
            if(FTP == null)
                throw new Exception("Invalid FTP ID");

            DTO.Log LogDTO = mapper.Map<DTO.Log>(log);
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
            if (log.TypeId == null)
                throw new FormatException("Invalid TypeId provided");
            if (log.FtpId == null)
                throw new FormatException("Invalid FtpId provided");
            if (log.Success == null)
                throw new FormatException("Invalid Success state provided");
            if (log.Message == null)
                throw new FormatException("Invalid Message provided");

            return await logRepository.AddLog(log);
        }

        public async Task<Models.Log> UpdateLog(DAO.Log targetLog)
        {
            Models.Log log = await logRepository.FindById((int)targetLog.id);
            if(log == null)
                throw new Exception("Invalid Log ID");
            return await logRepository.Update(log, targetLog);
        }
    }
}
