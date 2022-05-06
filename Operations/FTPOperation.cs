using AutoMapper;
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
        private IMapper mapper;
        public FTPOperation(FTPRepository ftpRepository, IMapper mapper)
        {
            this.ftpRepository = ftpRepository;
            this.mapper = mapper;
        }

        public async Task<List<Models.FTP>> GetAllFTPs(DAO.FilterFTP filter)
        {
            return await ftpRepository.GetAndFilter(filter);
        }

        private DTO.FTP MapFTP(Models.FTP ftp)
        {
            DTO.FTP FTPDTO = mapper.Map<DTO.FTP>(ftp);
            FTPDTO.DaysBackwards = TimeSpan.FromSeconds(ftp.DaysBackwards);

            return FTPDTO;
        }

        public async Task<DTO.FTP> Map(Models.FTP ftp)
        {
            return MapFTP(ftp);
        }

        public List<DTO.FTP> MapAll(List<Models.FTP> FTPs)
        {
            List<DTO.FTP> l = new List<DTO.FTP>();

            foreach (var ftp in FTPs)
                l.Add(MapFTP(ftp));

            return l;
        }

        public async Task DeleteFTP(int id)
        {
            await ftpRepository.DeleteFTP(id);
        }

        public async Task<Models.FTP> AddFTP(DAO.FTP ftp)
        {
            if (ftp.Name == null)
                throw new FormatException("Invalid Name provided");
            if (ftp.LoginHost == null)
                throw new FormatException("Invalid LoginHost provided");
            if (ftp.LoginName == null)
                throw new FormatException("Invalid LoginName provided");
            if (ftp.LoginPass == null)
                throw new FormatException("Invalid LoginPass provided");
            if (ftp.Directory == null)
                throw new FormatException("Invalid Directory provided");
            if (ftp.DaysBackwards == null)
                throw new FormatException("Invalid DaysBackwards provided");
            if (ftp.Pattern == null)
                throw new FormatException("Invalid Pattern provided");
            if (ftp.ActiveSince == null)
                throw new FormatException("Invalid ActiveSince Date provided");
            if (ftp.ActiveTo == null)
                throw new FormatException("Invalid ActiveTo Date provided");

            return await ftpRepository.AddFTP(ftp);
        }

        public async Task<Models.FTP> UpdateFTP(DAO.FTP targetFTP)
        {
            Models.FTP ftp = await ftpRepository.FindById((int)targetFTP.Id);
            if (ftp == null)
                throw new Exception("Invalid FTP ID");
            return await ftpRepository.Update(ftp, targetFTP);
        }
    }
}
