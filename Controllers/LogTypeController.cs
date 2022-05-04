using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newton_FTP_API.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newton_FTP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogTypeController : ControllerBase
    {
        private LogTypeOperation logTypeOperation;

        public LogTypeController(LogTypeOperation logTypeOperation)
        {
            this.logTypeOperation = logTypeOperation;
        }

        [HttpGet]
        public async Task<ActionResult<List<Models.LogType>>> GetAll()
        {
            return await logTypeOperation.GetAllTypes();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.LogType>> GetOne(int id)
        {
            return await logTypeOperation.GetOneType(id);
        }
    }
}
