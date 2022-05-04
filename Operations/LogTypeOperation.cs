using Newton_FTP_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newton_FTP_API.Operations
{
    public class LogTypeOperation
    {
        private LogTypeRepository logTypeRepository;
        public LogTypeOperation(LogTypeRepository logTypeRepository)
        {
            this.logTypeRepository = logTypeRepository;
        }
        public async Task<List<Models.LogType>> GetAllTypes()
        {
            return await logTypeRepository.FindAll();
        }
        public async Task<Models.LogType> GetOneType(int id)
        {
            return await logTypeRepository.FindById(id);
        }
    }
}
