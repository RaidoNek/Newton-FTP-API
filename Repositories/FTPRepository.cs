using AutoMapper;
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
        private IMapper mapper;
        public FTPRepository(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<Models.FTP> FindById(int id)
        {
            return await dataContext.FTPs.FindAsync(id);
        }

        public async Task<List<Models.FTP>> GetAndFilter(DAO.FilterFTP filter)
        {
            List<Models.FTP> FTPs = await dataContext.FTPs
                .Where(l => filter.Id == null || l.Id == filter.Id)
                .Where(l => filter.Pattern == null || l.Pattern == filter.Pattern)
                .Where(l => filter.isActive == null || (l.ActiveSince < DateTime.Now && l.ActiveTo > DateTime.Now))
                .Where(l => filter.CreationUser == null || l.CreationUser == filter.CreationUser)
                .ToListAsync();
            return FTPs;
        }

        public async Task DeleteFTP(int id)
        {
            Models.FTP ftp = await FindById(id);
            if(ftp == null)
                throw new Exception("Invalid FTP ID");
            dataContext.FTPs.Remove(ftp);
            await dataContext.SaveChangesAsync();
        }

        public async Task<Models.FTP> AddFTP(DAO.FTP ftp)
        {

            //asi jsem nemohl mapovat? Je tam variable se stejným jménem, ale jiným data typem, tak mi to řvalo, čili to udělám takhle radši :D :D 
            Models.FTP FTP = new Models.FTP
            {
                Name = ftp.Name,
                LoginHost = ftp.LoginHost,
                LoginName = ftp.LoginName,
                LoginPass = ftp.LoginPass,
                Directory = ftp.Directory,
                DaysBackwards = Convert.ToInt64(((TimeSpan)ftp.DaysBackwards).TotalSeconds),
                Pattern = ftp.Pattern,
                ActiveSince = (DateTime)ftp.ActiveSince,
                ActiveTo = (DateTime)ftp.ActiveTo,
                CreationDate = DateTime.Now,
                CreationUser = "AD USER"
            };

            dataContext.FTPs.Add(FTP);
            await dataContext.SaveChangesAsync();
            return FTP;
        }

        public async Task<Models.FTP> Update(Models.FTP ftp, DAO.FTP targetFTP)
        {
            ftp.Name = (targetFTP.Name == null) ? ftp.Name : targetFTP.Name;
            ftp.LoginHost = (targetFTP.LoginHost == null) ? ftp.LoginHost : targetFTP.LoginHost;
            ftp.LoginName = (targetFTP.LoginName == null) ? ftp.LoginName : targetFTP.LoginName;
            ftp.LoginPass = (targetFTP.LoginPass == null) ? ftp.LoginPass : targetFTP.LoginPass;
            ftp.Directory = (targetFTP.Directory == null) ? ftp.Directory : targetFTP.Directory;
            ftp.DaysBackwards = (targetFTP.DaysBackwards == null) ? ftp.DaysBackwards : Convert.ToInt64(((TimeSpan)targetFTP.DaysBackwards).TotalSeconds);
            ftp.Pattern = (targetFTP.Pattern == null) ? ftp.Pattern : targetFTP.Pattern;
            ftp.ActiveSince = (DateTime)((targetFTP.ActiveSince == null) ? ftp.ActiveSince : targetFTP.ActiveSince);
            ftp.ActiveTo = (DateTime)((targetFTP.ActiveTo == null) ? ftp.ActiveTo : targetFTP.ActiveTo);

            dataContext.Entry(ftp).State = EntityState.Modified;

            await dataContext.SaveChangesAsync();
            return ftp;
        }
    }
}
