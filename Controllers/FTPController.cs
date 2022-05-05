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
    public class FTPController : ControllerBase
    {
        private FTPOperation ftpOperation;

        public FTPController(FTPOperation ftpOperation)
        {
            this.ftpOperation = ftpOperation;
        }

        [HttpGet]
        public async Task<ActionResult<List<DTO.FTP>>> Get()
        {
            return await ftpOperation.GetAllFTPs();
        }

        //[HttpPost]
        //public async Task<ActionResult<Models.FTP>> AddOrUpdate()
        //{

        //}
    }
}
