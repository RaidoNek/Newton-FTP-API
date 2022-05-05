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

        public async Task<List<DTO.FTP>> GetAllFTPs()
        {
            var FTPs = await dataContext.FTPs.ToListAsync();
            var DTOFTPs = new List<DTO.FTP>();
            foreach(var FTP in FTPs)
            {
                var DTOFTP = mapper.Map<DTO.FTP>(FTP);
                DTOFTP.DaysBackwards = new TimeSpan(0, 0, 0, (int)FTP.DaysBackwards);
                DTOFTPs.Add(DTOFTP);
            }
            return DTOFTPs;
        }
    }
}
